-- Table: master_shema.users

-- DROP TABLE IF EXISTS master_shema.users;

CREATE TABLE IF NOT EXISTS master_shema.users
(
    id integer NOT NULL DEFAULT nextval('master_shema.users_id_seq'::regclass),
    name text COLLATE pg_catalog."default" NOT NULL,
    "first_name" text COLLATE pg_catalog."default" NOT NULL,
    "pfone" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT users_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master_shema.users
    OWNER to postgres;