-- Table: master_shema.orders

-- DROP TABLE IF EXISTS master_shema.orders;

CREATE TABLE IF NOT EXISTS master_shema.orders
(
    id integer NOT NULL DEFAULT nextval('master_shema.orders_id_seq'::regclass),
    user_id integer NOT NULL,
    CONSTRAINT orders_pkey PRIMARY KEY (id),
    CONSTRAINT user_fk FOREIGN KEY (user_id)
        REFERENCES master_shema.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master_shema.orders
    OWNER to postgres;
-- Index: fki_user_fk

-- DROP INDEX IF EXISTS master_shema.fki_user_fk;

CREATE INDEX IF NOT EXISTS fki_user_fk
    ON master_shema.orders USING btree
    (user_id ASC NULLS LAST)
    TABLESPACE pg_default;