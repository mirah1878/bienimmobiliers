\c postgres;
DROP DATABASE IF EXISTS mada_immo;
    CREATE DATABASE mada_immo;
    \c mada_immo;

    CREATE SEQUENCE admin_seq;
    CREATE TABLE IF NOT EXISTS admin (
        id VARCHAR PRIMARY KEY DEFAULT CONCAT('ADM', LPAD(nextval('admin_seq')::TEXT, 3, '0')),
        login VARCHAR NOT NULL NOT NULL,
        password VARCHAR NOT NULL
    );

    CREATE SEQUENCE proprietaire_seq;
    CREATE TABLE IF NOT EXISTS proprietaire (
        id VARCHAR PRIMARY KEY DEFAULT CONCAT('PRO', LPAD(nextval('proprietaire_seq')::TEXT, 3, '0')),
        tel VARCHAR UNIQUE NOT NULL
    );

    CREATE SEQUENCE client_seq;
    CREATE TABLE IF NOT EXISTS client (
        id VARCHAR PRIMARY KEY DEFAULT CONCAT('CLI', LPAD(nextval('client_seq')::TEXT, 3, '0')),
        email VARCHAR UNIQUE NOT NULL
    );

    CREATE SEQUENCE type_de_bien_seq;
    CREATE TABLE IF NOT EXISTS type_de_bien (
        id VARCHAR PRIMARY KEY DEFAULT CONCAT('TYP', LPAD(nextval('type_de_bien_seq')::TEXT, 3, '0')),
        nom VARCHAR NOT NULL,
        commission NUMERIC --en %
    );
    CREATE SEQUENCE region_seq;
    CREATE TABLE IF NOT EXISTS region(
        id VARCHAR PRIMARY KEY DEFAULT CONCAT('REG', LPAD(nextval('region_seq')::TEXT, 3, '0')),
        nom VARCHAR NOT NULL
    );
    
    CREATE SEQUENCE bien_seq;
    CREATE TABLE IF NOT EXISTS bien (
        id VARCHAR PRIMARY KEY DEFAULT CONCAT('BIEN', LPAD(nextval('bien_seq')::TEXT, 3, '0')),
        nom VARCHAR NOT NULL,
        description TEXT NOT NULL,
        loyer NUMERIC NOT NULL,
        id_proprietaire VARCHAR NOT NULL REFERENCES proprietaire(id),
        id_region VARCHAR NOT NULL REFERENCES region(id),
        id_type_de_bien VARCHAR NOT NULL REFERENCES type_de_bien(id)
    );
    CREATE SEQUENCE photo_seq;
    CREATE TABLE IF NOT EXISTS photo(
        id VARCHAR PRIMARY KEY DEFAULT CONCAT('PHT', LPAD(nextval('photo_seq')::TEXT, 3, '0')),
        nom TEXT NOT NULL,
        id_bien VARCHAR NOT NULL REFERENCES bien(id)
    );

    CREATE SEQUENCE location_seq;
    CREATE TABLE IF NOT EXISTS location (
        id VARCHAR PRIMARY KEY DEFAULT CONCAT('LOC', LPAD(nextval('location_seq')::TEXT, 3, '0')),
        id_bien VARCHAR NOT NULL REFERENCES bien(id),
        id_client VARCHAR NOT NULL REFERENCES client(id),
        duree INT NOT NULL, 
        date_debut DATE NOT NULL
    );