ALTER TABLE `user` ADD `external_uuid` VARCHAR(255) NOT NULL AFTER `username`;
CREATE INDEX `external_uuid_idx` ON `user` (`external_uuid`);
UPDATE `home_organizer`.`user` U JOIN `AuthServer`.`AspNetUser` AU ON AU.Email = U.email SET U.`external_uuid` = AU.Id;
ALTER TABLE `user` DROP `email`, DROP `password`;