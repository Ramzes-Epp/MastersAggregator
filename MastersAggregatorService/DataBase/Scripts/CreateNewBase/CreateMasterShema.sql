-- SCHEMA: master_shema

-- DROP SCHEMA IF EXISTS master_shema ;

CREATE SCHEMA IF NOT EXISTS master_shema
    AUTHORIZATION postgres;

COMMENT ON SCHEMA master_shema
    IS 'standard public schema';

GRANT ALL ON SCHEMA master_shema TO PUBLIC;

GRANT ALL ON SCHEMA master_shema TO postgres;