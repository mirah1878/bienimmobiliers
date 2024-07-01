CREATE OR REPLACE VIEW view_proprietaire_bien as
    SELECT 
        b.id as id_bien,
        b.nom as nom_bien ,
        b.description ,
        b.loyer ,
        b.id_proprietaire ,
        b.id_region ,
        b.id_type_de_bien ,
        pr.tel as telephone ,
        r.nom as nom_region ,
        tb.nom as nom_type_bien ,
        tb.commission 
    FROM bien as b
        JOIN proprietaire pr on b.id_proprietaire = pr.id
        JOIN region r ON b.id_region = r.id
        JOIN type_de_bien tb ON b.id_type_de_bien = tb.id; 



CREATE OR REPLACE VIEW view_chiffre_affaire AS
    SELECT 
        vpb.*,
        l.duree,
        l.date_debut,
        c.id AS id_client,
        c.email,
        vpb.loyer * l.duree AS chiffre_affaire,
        ROUND((vpb.loyer * l.duree) * (vpb.commission / 100), 2) AS gain,
        ROUND((vpb.loyer * (vpb.commission / 100)), 2) AS gain_par_mois,
        DENSE_RANK() OVER(PARTITION BY vpb.id_bien, c.id ORDER BY ((date_trunc('month', (l.date_debut + (i || ' month')::interval)) + INTERVAL '1 month - 1 day')::date))::INTEGER AS mois,
        (date_trunc('month', (l.date_debut + (i || ' month')::interval)) + INTERVAL '1 month - 1 day')::date AS fin_du_mois,
        date_trunc('month', (l.date_debut + (i || ' month')::interval))::date AS mois_loyer
    FROM location AS l
        LEFT JOIN view_proprietaire_bien vpb ON l.id_bien = vpb.id_bien
        LEFT JOIN client c ON l.id_client = c.id
        CROSS JOIN generate_series(0, l.duree - 1) AS i;

CREATE OR REPLACE VIEW view_detail_location AS
    SELECT nom_bien , commission, mois,date_debut,fin_du_mois,loyer, gain,chiffre_affaire,email from view_chiffre_affaire;

CREATE OR REPLACE VIEW view_ca AS
SELECT 
        vl.*,
        CASE WHEN vl.mois = 1 THEN (vl.loyer * 2) ELSE vl.loyer END AS loyer_payer,
        CASE WHEN vl.mois = 1 THEN (50.0)::NUMERIC ELSE vl.commission END AS commission_pourcentage,
        CASE WHEN vl.mois = 1 THEN ((vl.loyer * 2) * 50)/100 ELSE (vl.loyer * vl.commission)/100 END AS valeur_commission 
    FROM view_chiffre_affaire as vl;


