DELIMITER $$
--
-- Procedury
--
DROP PROCEDURE IF EXISTS `add_group`$$
CREATE DEFINER=`rado`@`%` PROCEDURE `add_group` (IN `userUuid` BINARY(16), IN `name` VARCHAR(255), OUT `groupUuid` BINARY(16))  BEGIN
	SET groupUuid = UNHEX(REPLACE(UUID(), '-', ''));  
    INSERT INTO `group` (`uuid`, `name`, `create_time`, `update_time`, `delete_time`) VALUES
	(groupUuid, name, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    SET @userGroupUuid = UNHEX(REPLACE(UUID(), '-', ''));
    INSERT INTO `user_groups` (`uuid`, `user_uuid`, `group_uuid`, `create_time`, `update_time`, `delete_time`) VALUES
	(@userGroupUuid, userUuid, groupUuid, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    INSERT INTO `expenses_settings` (`uuid`, `user_groups_uuid`, `value`, `create_time`, `update_time`, `delete_time`) VALUES
	(UNHEX(REPLACE(UUID(), '-', '')), @userGroupUuid, 1, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    INSERT INTO `shopping_list` (`uuid`, `group_uuid`, `name`, `description`, `visible`, `create_time`, `update_time`, `delete_time`) VALUES
	(UNHEX(REPLACE(UUID(), '-', '')), groupUuid, 'GROUP_ONE_TIME', 'GROUP_ONE_TIME', 1, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    SET @categoryUuid = UNHEX(REPLACE(UUID(), '-', ''));
    INSERT INTO `category` (`uuid`, `group_uuid`, `name`, `create_time`, `update_time`, `delete_time`) VALUES
	(@categoryUuid, groupUuid, 'none', CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    INSERT INTO `subcategory` (`uuid`, `group_uuid`, `name`, `category_uuid`, `create_time`, `update_time`, `delete_time`) VALUES
	(UNHEX(REPLACE(UUID(), '-', '')), groupUuid, 'none', @categoryUuid, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
    INSERT INTO `expenses` (`uuid`, `group_uuid`, `name`, `create_time`, `update_time`, `delete_time`) VALUES
    (UNHEX(REPLACE(UUID(), '-', '')), groupUuid, 'init', CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
END$$

DROP PROCEDURE IF EXISTS `add_user`$$
CREATE DEFINER=`rado`@`%` PROCEDURE `add_user` (IN `externalUuid` VARCHAR(255), IN `name` VARCHAR(255))  BEGIN
	INSERT INTO `user` (`uuid`, `username`, `external_uuid`, `create_time`, `update_time`, `delete_time`) VALUES
	(UNHEX(REPLACE(UUID(), '-', '')), name, externalUuid, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
END$$

DROP PROCEDURE IF EXISTS `add_user_to_group`$$
CREATE DEFINER=`rado`@`localhost` PROCEDURE `add_user_to_group` (IN `userUuid` BINARY(16), IN `groupUuid` BINARY(16))  BEGIN
	SET @userGroupUuid = UNHEX(REPLACE(UUID(), '-', ''));
    INSERT INTO `user_groups` (`uuid`, `user_uuid`, `group_uuid`, `create_time`, `update_time`, `delete_time`) VALUES
	(@userGroupUuid, userUuid, groupUuid, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
      
    INSERT INTO `expenses_settings` (`uuid`, `user_groups_uuid`, `value`, `create_time`, `update_time`, `delete_time`) VALUES
	(UNHEX(REPLACE(UUID(), '-', '')), @userGroupUuid, 1, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);
    
   	SET @size = (SELECT Count(*) FROM `user` u
    JOIN `user_groups` ug on ug.`user_uuid` = u.`uuid`
    JOIN `group` g on g.uuid = ug.group_uuid AND g.uuid = groupUuid);
    
    SET @prop = 1/@size;
    
    UPDATE `expenses_settings` es
    JOIN `user_groups` ug ON ug.uuid = es.user_groups_uuid
	SET `value` = @prop
	WHERE ug.group_uuid = groupUuid;
    
	SELECT @tmpUserUuid := 0x00000000000000000000000000000000;
    SELECT @tmpTimestamp := '0000-00-00 00:00:00';
    
    SET @expenseUuid = (SELECT e.`uuid` FROM `expenses` e WHERE `name`='init' AND `group_uuid`= groupUuid);
        
    SET @i = 0;
	loop_1: WHILE @i<@size DO  
        SET @i = @i + 1;
                
  		SELECT @tmpUserUuid := u.`uuid`, @tmpTimestamp := u.`create_time`
  		FROM `user` u
        JOIN `user_groups` ug on ug.user_uuid = u.uuid
        JOIN `group` g on g.uuid = ug.group_uuid AND g.uuid = groupUuid
  		WHERE u.`create_time` > @tmpTimestamp
  		ORDER BY u.`create_time`
        LIMIT 1;

		IF @tmpUserUuid = userUuid THEN
            ITERATE loop_1;
        END IF;
  		INSERT INTO `expense_details` (`uuid`, `expense_uuid`, `value`, `payer_uuid`, `recipient_uuid`, `create_time`, `update_time`, `delete_time`) VALUES
		(UNHEX(REPLACE(UUID(), '-', '')), @expenseUuid, '0', @tmpUserUuid, userUuid, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL),
		(UNHEX(REPLACE(UUID(), '-', '')), @expenseUuid, '0', userUuid, @tmpUserUuid, CURRENT_TIMESTAMP(), CURRENT_TIMESTAMP(), NULL);

	END WHILE;
END$$

DELIMITER ;