-- Insertion de données dans la table admin
INSERT INTO admin (login, password) VALUES
('admin1', '12345'),
('admin2', '54321');

-- Insertion de données dans la table proprietaire
INSERT INTO proprietaire (tel) VALUES
('0326851080'),
('0341676690');

-- Insertion de données dans la table client
INSERT INTO client (email) VALUES
('nelly@gmail.com'),
('anouchka@gmail.com');

INSERT INTO client (email) VALUES
('Mirah@gmail.com');

-- Insertion de données dans la table type_de_bien
INSERT INTO type_de_bien (nom, commission) VALUES
('Maison', 5.0),
('Appartement', 4.5),
('Villa', 6.0),
('Immeuble', 7.0);

-- Insertion de données dans la table region
INSERT INTO region (nom) VALUES
('Nord'),
('Est'),
('Sud'),
('Ouest');

-- Insertion de données dans la table bien
INSERT INTO bien (nom, description, loyer, id_proprietaire, id_region, id_type_de_bien) VALUES
('Maison du Lac', 'Maison spacieuse avec vue sur le lac', 600000, 'PRO001', 'REG001', 'TYP001'),
('Appartement Centre', 'Appartement moderne en centre-ville', 800000, 'PRO002', 'REG002', 'TYP002');

-- Insertion de données dans la table photo
INSERT INTO photo (nom, id_bien) VALUES
('maison_du_lac.jpg', 'BIEN001'),
('appartement_centre.jpg', 'BIEN002');

-- Insertion de données dans la table location
INSERT INTO location (id_bien, id_client, duree, date_debut) VALUES
('BIEN001', 'CLI001', 12, '2024-07-01'),
('BIEN002', 'CLI002', 6, '2024-08-01');

INSERT INTO location (id_bien, id_client, duree, date_debut) VALUES
('BIEN002', 'CLI003', 12, '2024-08-12');

INSERT INTO location (id_bien, id_client, duree, date_debut) VALUES
('BIEN002', 'CLI001', 3, '2024-03-12'),
('BIEN001', 'CLI001', 4, '2024-04-10'),
('BIEN002', 'CLI001', 2, '2024-05-19');

