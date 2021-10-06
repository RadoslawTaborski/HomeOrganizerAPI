ALTER TABLE `user_groups` ADD `owner` TINYINT NOT NULL AFTER `group_uuid`;
CREATE INDEX `owner_idx` ON `user_groups` (`owner`);