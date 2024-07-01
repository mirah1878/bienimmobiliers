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
        tb.commission ,
        ph.id as id_photo,
        ph.nom 
    FROM bien as b
        JOIN proprietaire pr on b.id_proprietaire = pr.id
        JOIN region r ON b.id_region = r.id
        JOIN type_de_bien tb ON b.id_type_de_bien = tb.id
        JOIN photo ph ON b.id = ph.id_bien; 



CREATE OR REPLACE VIEW view_chiffre_affaire AS
    SELECT 
        vpb.*,
        l.duree,
        l.date_debut,
        c.id AS id_client,
        c.email,
        vpb.loyer * l.duree AS chiffre_affaire,
        ROUND((vpb.loyer * l.duree) * (vpb.commission / 100), 2) AS gain
    FROM location AS l
        LEFT JOIN view_proprietaire_bien vpb ON l.id_bien = vpb.id_bien
        LEFT JOIN client c ON l.id_client = c.id;


    SELECT
        id_client,loyer,date_debut,duree,chiffre_affaire from view_chiffre_affaire 
        where date_debut >= '2024-06-07' and date_debut <= '2024-12-30';


    SELECT
        id_client,
        loyer,
        date_debut,
        duree,
        chiffre_affaire,
        (DATE_PART('year', '2024-01-30'::date) * 12 + DATE_PART('month', '2024-01-30'::date)) - (DATE_PART('year', '2024-2-01'::date) * 12 + DATE_PART('month', '2024-02-01'::date)) AS countMonth
    FROM
        view_chiffre_affaire
    WHERE
        date_debut >= '2024-06-07' 
        AND date_debut <= '2024-12-30';
















SELECT
    id_client,
    loyer,
    date_debut,
    duree,
    chiffre_affaire,
    (DATE_PART('year', age('2024-02-01'::date, '2024-01-31'::date)) * 12 +
     DATE_PART('month', age('2024-02-01'::date, '2024-01-31'::date)) + 1) AS countMonth
FROM
    view_chiffre_affaire

    