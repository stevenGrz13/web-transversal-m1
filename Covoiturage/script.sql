-- ============================================
-- TABLE : role
-- ============================================
CREATE TABLE role (
                      id SERIAL PRIMARY KEY,
                      nom VARCHAR(255) NOT NULL
);

-- ============================================
-- TABLE : utilisateur
-- ============================================
CREATE TABLE utilisateur (
                             id SERIAL PRIMARY KEY,
                             idrole INT NOT NULL,
                             nom VARCHAR(255),
                             email VARCHAR(255),
                             motdepasse VARCHAR(255),
                             numero VARCHAR(50),
                             createdAt TIMESTAMP WITHOUT TIME ZONE DEFAULT CURRENT_TIMESTAMP,
                             CONSTRAINT fk_utilisateur_role FOREIGN KEY (idrole)
                                 REFERENCES role (id)
                                 ON UPDATE CASCADE ON DELETE RESTRICT
);

-- ============================================
-- TABLE : vehicule
-- ============================================
CREATE TABLE vehicule (
                          id SERIAL PRIMARY KEY,
                          idutilisateur INT NOT NULL,
                          marque VARCHAR(255),
                          modele VARCHAR(255),
                          plaque VARCHAR(50),
                          nombreplace INT,
                          CONSTRAINT fk_vehicule_utilisateur FOREIGN KEY (idutilisateur)
                              REFERENCES utilisateur (id)
                              ON UPDATE CASCADE ON DELETE CASCADE
);

-- ============================================
-- TABLE : trajet
-- ============================================
CREATE TABLE trajet (
                        id SERIAL PRIMARY KEY,
                        idvehicule INT NOT NULL,
                        depart VARCHAR(255),
                        arrivee VARCHAR(255),
                        datedepart TIMESTAMP WITHOUT TIME ZONE,
                        prixuniqueplace INT,
                        CONSTRAINT fk_trajet_vehicule FOREIGN KEY (idvehicule)
                            REFERENCES vehicule (id)
                            ON UPDATE CASCADE ON DELETE CASCADE
);

-- ============================================
-- TABLE : voyage
-- ============================================
CREATE TABLE voyage (
                        id SERIAL PRIMARY KEY,
                        idpassager INT NOT NULL,
                        idtrajet INT NOT NULL,
                        lieurecuperation VARCHAR(255),
                        destination VARCHAR(255),
                        estpayee BOOLEAN DEFAULT FALSE,
                        CONSTRAINT fk_voyage_passager FOREIGN KEY (idpassager)
                            REFERENCES utilisateur (id)
                            ON UPDATE CASCADE ON DELETE CASCADE,
                        CONSTRAINT fk_voyage_trajet FOREIGN KEY (idtrajet)
                            REFERENCES trajet (id)
                            ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE depense (
                         id SERIAL PRIMARY KEY,
                         intitule INTEGER NOT NULL,
                         montant DOUBLE PRECISION NOT NULL,
                         datepaiement TIMESTAMP NOT NULL
);

CREATE TABLE commission (
                         id SERIAL PRIMARY KEY,
                         pourcentage DOUBLE PRECISION NOT NULL,
                         datedecision TIMESTAMP NOT NULL
);