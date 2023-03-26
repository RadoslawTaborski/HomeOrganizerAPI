CREATE TABLE `list_category` (
  `uuid` binary(16) NOT NULL,
  `group_uuid` binary(16) NOT NULL,
  `name` varchar(60) COLLATE utf8_polish_ci NOT NULL,
  `create_time` timestamp NOT NULL DEFAULT current_timestamp(),
  `update_time` timestamp NULL DEFAULT NULL,
  `delete_time` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`uuid`),
  UNIQUE KEY `uuid` (`uuid`),
  KEY `fk_list_category_group1_idx` (`group_uuid`),
  CONSTRAINT `fk_list_category_group1` FOREIGN KEY (`group_uuid`) REFERENCES `group` (`uuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

ALTER TABLE `shopping_list` ADD `category_uuid` BINARY(16) NULL AFTER `description`,
ADD CONSTRAINT `fk_shopping_list_category1` FOREIGN KEY (`category_uuid`) 
REFERENCES `list_category`(`uuid`);

INSERT INTO `list_category` (`uuid`, `group_uuid`, `name`, `create_time`, `update_time`, `delete_time`) SELECT
UNHEX(REPLACE(UUID(), '-', '')), uuid, 'none', CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL FROM `group`;

UPDATE `shopping_list` L
JOIN `list_category` C ON C.`group_uuid` = L.`group_uuid` AND C.name = 'none' 
SET L.`category_uuid` = C.uuid;

ALTER TABLE `shopping_list` MODIFY `category_uuid` BINARY(16) NOT NULL;

--
-- Procedury
--
DELIMITER $$
DROP PROCEDURE IF EXISTS `add_group`$$
CREATE DEFINER=`rado`@`%` PROCEDURE `add_group` (IN `userUuid` BINARY(16), IN `name` VARCHAR(255), OUT `groupUuid` BINARY(16))  BEGIN
	SET groupUuid = UNHEX(REPLACE(UUID(), '-', ''));  
    INSERT INTO `group` (`uuid`, `name`, `create_time`, `update_time`, `delete_time`) VALUES
	(groupUuid, name, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    SET @userGroupUuid = UNHEX(REPLACE(UUID(), '-', ''));
    INSERT INTO `user_groups` (`uuid`, `user_uuid`, `group_uuid`, `owner`, `create_time`, `update_time`, `delete_time`) VALUES
	(@userGroupUuid, userUuid, groupUuid, 1, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    INSERT INTO `expenses_settings` (`uuid`, `user_groups_uuid`, `value`, `create_time`, `update_time`, `delete_time`) VALUES
	(UNHEX(REPLACE(UUID(), '-', '')), @userGroupUuid, 1, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    SET @listCategoryUuid = UNHEX(REPLACE(UUID(), '-', ''));
    INSERT INTO `list_category` (`uuid`, `group_uuid`, `name`, `create_time`, `update_time`, `delete_time`) VALUES
	(@listCategoryUuid, groupUuid, 'none', CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);

    INSERT INTO `shopping_list` (`uuid`, `group_uuid`, `name`, `description`, `category_uuid`, `visible`, `create_time`, `update_time`, `delete_time`) VALUES
	(UNHEX(REPLACE(UUID(), '-', '')), groupUuid, 'GROUP_ONE_TIME', 'GROUP_ONE_TIME', @listCategoryUuid, 1, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    SET @categoryUuid = UNHEX(REPLACE(UUID(), '-', ''));
    INSERT INTO `category` (`uuid`, `group_uuid`, `name`, `create_time`, `update_time`, `delete_time`) VALUES
	(@categoryUuid, groupUuid, 'none', CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    INSERT INTO `subcategory` (`uuid`, `group_uuid`, `name`, `category_uuid`, `create_time`, `update_time`, `delete_time`) VALUES
	(UNHEX(REPLACE(UUID(), '-', '')), groupUuid, 'none', @categoryUuid, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    INSERT INTO `expenses` (`uuid`, `group_uuid`, `name`, `create_time`, `update_time`, `delete_time`) VALUES
    (UNHEX(REPLACE(UUID(), '-', '')), groupUuid, 'init', CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
END$$