-- Table: master_shema.image_of_orders

-- DROP TABLE IF EXISTS master_shema.image_of_orders;

CREATE TABLE IF NOT EXISTS master_shema.image_of_orders
(
    id integer NOT NULL DEFAULT nextval('master_shema.image_of_orders_id_seq'::regclass),
    order_id integer NOT NULL,
    image_id integer NOT NULL,
    CONSTRAINT image_of_orders_pkey PRIMARY KEY (id),
    CONSTRAINT image_of_orders_unique UNIQUE (order_id, image_id),
    CONSTRAINT image_fk FOREIGN KEY (image_id)
        REFERENCES master_shema.images (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT order_fk FOREIGN KEY (order_id)
        REFERENCES master_shema.orders (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS master_shema.image_of_orders
    OWNER to postgres;