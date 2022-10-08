-- Table: master_shema.masters

-- DROP TABLE IF EXISTS master_shema.masters;

CREATE TABLE IF NOT EXISTS master_shema.masters
(
    id integer NOT NULL DEFAULT nextval('master_shema.masters_id_seq'::regclass),
    name text COLLATE pg_catalog."default" NOT NULL,
    is_active boolean NOT NULL DEFAULT false,
    CONSTRAINT masters_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master_shema.masters
    OWNER to postgres;