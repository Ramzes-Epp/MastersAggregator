-- Table: master_shema.images

-- DROP TABLE IF EXISTS master_shema.images;

CREATE TABLE IF NOT EXISTS master_shema.images
(
    id integer NOT NULL DEFAULT nextval('master_shema.images_id_seq'::regclass),
    url text COLLATE pg_catalog."default" NOT NULL,
    description text COLLATE pg_catalog."default",
    CONSTRAINT images_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master_shema.images
    OWNER to postgres;