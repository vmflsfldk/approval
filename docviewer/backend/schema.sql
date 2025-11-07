CREATE DATABASE IF NOT EXISTS `approvaldb` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `approvaldb`;

CREATE TABLE IF NOT EXISTS `docs` (
  `id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `file_path` VARCHAR(1024) NOT NULL,
  `file_name` VARCHAR(512) NOT NULL,
  `owner_user` VARCHAR(190) NOT NULL DEFAULT '',
  `doc_no` VARCHAR(190) NULL,
  `created_date` DATE NOT NULL,
  `created_ts` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_ym` CHAR(7) AS (CONCAT(EXTRACT(YEAR FROM created_date), '-', LPAD(EXTRACT(MONTH FROM created_date), 2, '0'))) STORED,
  PRIMARY KEY (`id`),
  UNIQUE KEY `uq_docs_file_path` (`file_path`),
  KEY `idx_docs_owner_date_id` (`owner_user`, `created_date`, `id`),
  KEY `idx_docs_created_date_id` (`created_date`, `id`),
  KEY `idx_docs_created_ym_id` (`created_ym`, `id`),
  KEY `idx_docs_doc_no` (`doc_no`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

DROP INDEX IF EXISTS `ft_docs_file_docno` ON `docs`;
ALTER TABLE `docs`
  ADD FULLTEXT KEY `ft_docs_file_docno` (`file_name`, `doc_no`);

CREATE TABLE IF NOT EXISTS `users` (
  `username` VARCHAR(190) NOT NULL,
  `name` VARCHAR(190) NOT NULL,
  `password_hash` VARCHAR(255) NOT NULL,
  `role` ENUM('admin','user') NOT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

CREATE TABLE IF NOT EXISTS `audit_logs` (
  `id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `user` VARCHAR(190) NOT NULL,
  `action` ENUM('LOGIN','SEARCH','VIEW','DOWNLOAD') NOT NULL,
  `doc_id` BIGINT UNSIGNED NULL,
  `q` VARCHAR(512) NULL,
  `ts` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `idx_audit_user_ts` (`user`, `ts`),
  KEY `idx_audit_doc_ts` (`doc_id`, `ts`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
