CREATE DATABASE IF NOT EXISTS alfn;

CREATE TABLE IF NOT EXISTS alfn.primes (
    generated_time DateTime,
    entry_time DateTime,
    nickname String,
    number UInt64
) ENGINE = MergeTree()
ORDER BY generated_time;

CREATE TABLE IF NOT EXISTS alfn.primes_kafka (
    generated_time DateTime,
    entry_time DateTime,
    nickname String,
    number UInt64
) ENGINE = Kafka(
    'kafka:9092',         -- broker
    'primes',        -- topic
    'click-primes-group', -- group
    'JSONEachRow'         -- format
);


CREATE MATERIALIZED VIEW alfn.primes_mv TO alfn.primes AS
    SELECT * FROM alfn.primes_kafka;