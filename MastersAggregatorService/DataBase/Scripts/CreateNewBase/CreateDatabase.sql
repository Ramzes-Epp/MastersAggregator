-- Database: masters_aggregator_database

-- DROP DATABASE IF EXISTS masters_aggregator_database;

CREATE DATABASE masters_aggregator_database
    WITH
    OWNER = web_api_service
    ENCODING = 'UTF8'
    LC_COLLATE = 'C.UTF-8'
    LC_CTYPE = 'C.UTF-8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;