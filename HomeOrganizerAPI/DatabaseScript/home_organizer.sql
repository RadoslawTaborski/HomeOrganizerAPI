-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema home_organizer
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema home_organizer
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `home_organizer` DEFAULT CHARACTER SET utf8 COLLATE utf8_polish_ci ;
USE `home_organizer` ;


-- -----------------------------------------------------
-- Table `home_organizer`.`shopping_list`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`shopping_list` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(60) NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NULL,
  `delete_time` TIMESTAMP NULL,
  PRIMARY KEY (`id`)
)
ENGINE = InnoDB;

-- -----------------------------------------------------
-- Table `home_organizer`.`category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`category` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(60) NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NULL,
  `delete_time` TIMESTAMP NULL,
  PRIMARY KEY (`id`)
);


-- -----------------------------------------------------
-- Table `home_organizer`.`subcategory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`subcategory` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(60) NOT NULL,
  `category_id` INT NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NULL,
  `delete_time` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  KEY `fk_subcategory_category1_idx` (`category_id`)
);
	
ALTER TABLE `home_organizer`.`subcategory`
ADD CONSTRAINT `fk_subcategory_category1` FOREIGN KEY (`category_id`) REFERENCES `home_organizer`.`category` (`id`);


-- -----------------------------------------------------
-- Table `home_organizer`.`state`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`state` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(60) NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NULL,
  `delete_time` TIMESTAMP NULL,
  PRIMARY KEY (`id`)
)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `home_organizer`.`item`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`item` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(60) NOT NULL,
  `shopping_list_id` INT NULL,
  `state_id` INT NULL,
  `quantity` VARCHAR(60) NULL,
  `category_id` INT NOT NULL,
  `bought` TIMESTAMP NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NULL,
  `delete_time` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  KEY `fk_item_subcategory1_idx` (`category_id`),
  KEY `fk_item_state1_idx` (`state_id`),
  KEY `fk_item_shopping_list1_idx` (`shopping_list_id`)
)
ENGINE = InnoDB;

ALTER TABLE `home_organizer`.`item`
ADD CONSTRAINT `fk_item_subcategory1` FOREIGN KEY (`category_id`) REFERENCES `home_organizer`.`subcategory` (`id`),
ADD CONSTRAINT `fk_item_state1` FOREIGN KEY (`state_id`) REFERENCES `home_organizer`.`state` (`id`),
ADD CONSTRAINT `fk_item_shopping_list1` FOREIGN KEY (`shopping_list_id`) REFERENCES `home_organizer`.`shopping_list` (`id`);

-- -----------------------------------------------------
-- Table `home_organizer`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`user` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(16) NOT NULL,
  `email` VARCHAR(255) NULL,
  `password` VARCHAR(32) NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NULL,
  `delete_time` TIMESTAMP NULL,
  PRIMARY KEY (`id`)
);

-- -----------------------------------------------------
-- Table `home_organizer`.`expenses`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`expenses` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(60) NOT NULL,
  `value` DECIMAL NOT NULL,
  `payer_id` INT NOT NULL,
  `recipient_id` INT NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NULL,
  `delete_time` TIMESTAMP NULL,
  PRIMARY KEY (`id`),
  KEY `fk_expenses_user1_idx` (`payer_id`),
  KEY `fk_expenses_user2_idx` (`recipient_id`)
)
ENGINE = InnoDB;

ALTER TABLE `home_organizer`.`expenses`
ADD CONSTRAINT `fk_expenses_user1` FOREIGN KEY (`payer_id`) REFERENCES `home_organizer`.`user` (`id`),
ADD CONSTRAINT `fk_expenses_user2` FOREIGN KEY (`recipient_id`) REFERENCES `home_organizer`.`user` (`id`);

-- -----------------------------------------------------
-- Table `home_organizer`.`expenses_settings`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`expenses_settings` (
  `user1_id` INT NOT NULL,
  `user2_id` INT NOT NULL,
  `value` FLOAT NOT NULL,
  `create_time` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `update_time` TIMESTAMP NULL,
  `delete_time` TIMESTAMP NULL,
  KEY `fk_expenses_settings_user1_idx` (`user1_id`),
  KEY `fk_expenses_settings_user2_idx` (`user2_id`),
  PRIMARY KEY (`user1_id`, `user2_id`)
)
ENGINE = InnoDB;

ALTER TABLE `home_organizer`.`expenses_settings`
ADD CONSTRAINT `fk_expenses_settings_user1` FOREIGN KEY (`user1_id`) REFERENCES `home_organizer`.`user` (`id`),
ADD CONSTRAINT `fk_expenses_settings_user2` FOREIGN KEY (`user2_id`) REFERENCES `home_organizer`.`user` (`id`);

USE `home_organizer` ;

-- -----------------------------------------------------
-- Placeholder table for view `home_organizer`.`permanent_item`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`permanent_item` (`id` INT, `name` INT, `satate_id` INT, `category_id` INT, `timestamps` INT);

-- -----------------------------------------------------
-- Placeholder table for view `home_organizer`.`temporary_item`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`temporary_item` (`id` INT, `name` INT, `shopping_list_id` INT, `quantity` INT, `category_id` INT, `bought` INT, `timestamps` INT);

-- -----------------------------------------------------
-- Placeholder table for view `home_organizer`.`saldo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `home_organizer`.`saldo` (`value` INT);

-- -----------------------------------------------------
-- View `home_organizer`.`permanent_item`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `home_organizer`.`permanent_item`;
USE `home_organizer`;
CREATE  OR REPLACE VIEW `permanent_item` AS
SELECT `item`.`id`, `item`.`name`, `item`.`state_id`, `item`.`category_id`, `item`.`create_time`, `item`.`update_time`, `item`.`delete_time`
FROM item
WHERE shopping_list_id IS NULL;

-- -----------------------------------------------------
-- View `home_organizer`.`temporary_item`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `home_organizer`.`temporary_item`;
USE `home_organizer`;
CREATE  OR REPLACE VIEW `temporary_item` AS
SELECT `item`.`id`, `item`.`name`, `item`.`shopping_list_id`, `item`.`quantity`, `item`.`category_id`, `item`.`bought`, `item`.`create_time`, `item`.`update_time`, `item`.`delete_time`
FROM item 
WHERE shopping_list_id IS NOT NULL;

-- -----------------------------------------------------
-- View `home_organizer`.`saldo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `home_organizer`.`saldo`;
USE `home_organizer`;
CREATE  OR REPLACE VIEW `saldo` AS
SELECT `payer_id`, `recipient_id`, `expenses`.`value`
FROM expenses;

-- -----------------------------------------------------
-- Insert datas
-- -----------------------------------------------------

INSERT INTO `category` (`id`, `name`, `create_time`, `update_time`, `delete_time`) VALUES
(1, 'spożywcze', CURRENT_TIMESTAMP, null, null),
(2, 'chemia', CURRENT_TIMESTAMP, null, null),
(3, 'odzież', CURRENT_TIMESTAMP, null, null),
(4, 'agd', CURRENT_TIMESTAMP, null, null),
(5, 'narzędzia', CURRENT_TIMESTAMP, null, null);

INSERT INTO `subcategory` (`id`, `name`, `category_id`, `create_time`, `update_time`, `delete_time`) VALUES
(1, 'owoce i warzywa', 1, CURRENT_TIMESTAMP, null, null),
(2, 'nabiał', 1, CURRENT_TIMESTAMP, null, null),
(3, 'konserwy', 1, CURRENT_TIMESTAMP, null, null),
(4, 'pieczywo', 1, CURRENT_TIMESTAMP, null, null),
(5, 'alkohol', 1, CURRENT_TIMESTAMP, null, null),
(6, 'przekąski', 1, CURRENT_TIMESTAMP, null, null),
(7, 'mięso', 1, CURRENT_TIMESTAMP, null, null),
(8, 'produkty zbożowe', 1, CURRENT_TIMESTAMP, null, null),
(9, 'przyprawy', 1, CURRENT_TIMESTAMP, null, null),
(10, 'sosy', 1, CURRENT_TIMESTAMP, null, null),
(11, 'kuchnia', 2, CURRENT_TIMESTAMP, null, null),
(12, 'łazienka', 2, CURRENT_TIMESTAMP, null, null),
(13, 'higiena', 2, CURRENT_TIMESTAMP, null, null),
(14, 'intymne', 2, CURRENT_TIMESTAMP, null, null),
(15, 'odzież', 3, CURRENT_TIMESTAMP, null, null),
(16, 'agd', 4, CURRENT_TIMESTAMP, null, null),
(17, 'narzędzia', 5, CURRENT_TIMESTAMP, null, null),
(18, 'salon', 2, CURRENT_TIMESTAMP, null, null);

INSERT INTO `state` (`id`, `name`, `create_time`, `update_time`, `delete_time`) VALUES
(1, 'CRITICAL', CURRENT_TIMESTAMP, null, null),
(2, 'LITTLE', CURRENT_TIMESTAMP, null, null),
(3, 'MEDIUM', CURRENT_TIMESTAMP, null, null),
(4, 'LOT', CURRENT_TIMESTAMP, null, null);

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
