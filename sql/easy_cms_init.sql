/*
Navicat MySQL Data Transfer


Date: 2018-12-03 22:43:24
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for admin_area
-- ----------------------------
DROP TABLE IF EXISTS `admin_area`;
CREATE TABLE `admin_area` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of admin_area
-- ----------------------------

-- ----------------------------
-- Table structure for admin_config
-- ----------------------------
DROP TABLE IF EXISTS `admin_config`;
CREATE TABLE `admin_config` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `system_name` longtext,
  `is_data_inited` bit(1) NOT NULL,
  `data_init_date` datetime(6) NOT NULL,
  `is_delete` bit(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of admin_config
-- ----------------------------
INSERT INTO `admin_config` VALUES ('ed5e6f4f-b373-413d-b8a0-c7dc38fcb038', '2018-02-10 00:00:00.000000', 'Boxhi Manager', '', '2018-02-10 00:00:00.000000', '\0');

-- ----------------------------
-- Table structure for admin_login_log
-- ----------------------------
DROP TABLE IF EXISTS `admin_login_log`;
CREATE TABLE `admin_login_log` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `user_id` char(36) NOT NULL,
  `login_name` varchar(50) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  `message` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_admin_login_log_user_id` (`user_id`) USING BTREE,
  CONSTRAINT `admin_login_log_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `admin_user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of admin_login_log
-- ----------------------------
INSERT INTO `admin_login_log` VALUES ('465439cb-6cec-4c77-8c43-956cf67ba252', '2018-12-03 22:24:45.012100', 'f5cf28ca-d5c4-c12f-804f-08d614d69906', 'superadmin', '::1', '登录成功');
INSERT INTO `admin_login_log` VALUES ('485ccc07-cee6-4500-8d13-1d423c04654f', '2018-11-25 20:18:41.948085', 'f5cf28ca-d5c4-c12f-804f-08d614d69906', 'superadmin', '::1', '登录成功');

-- ----------------------------
-- Table structure for admin_menu
-- ----------------------------
DROP TABLE IF EXISTS `admin_menu`;
CREATE TABLE `admin_menu` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `parent_id` char(36) DEFAULT NULL,
  `code` varchar(50) DEFAULT NULL,
  `path_code` varchar(50) DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `url` varchar(100) DEFAULT NULL,
  `order` int(11) NOT NULL,
  `type` tinyint(3) unsigned NOT NULL,
  `icon` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of admin_menu
-- ----------------------------
INSERT INTO `admin_menu` VALUES ('19549426-882b-461b-b73f-f7b9917b565e', '2018-02-10 00:00:00.000000', null, 'AA', 'AA', '系统设置', '#', '1', '1', 'fa fa-home');
INSERT INTO `admin_menu` VALUES ('1fa642ab-f20b-44d2-a400-d19a1758f0ef', '2018-02-10 00:00:00.000000', 'a7865d53-3cb8-48c1-b906-66bb363c6af1', 'AC', 'AAAAAC', '删除菜单', '/Admin/Menu/RemoveForm', '14', '3', null);
INSERT INTO `admin_menu` VALUES ('39028f62-460a-4522-a337-70978d00819b', '2018-02-10 00:00:00.000000', '9d3aa3eb-d00a-4890-9d8a-1a46a7db92ec', 'AB', 'ABAB', '访问日志', '/Admin/SystemLog/Visits', '2', '2', 'fa fa-forumbee');
INSERT INTO `admin_menu` VALUES ('39e9f9f7-850b-0e0d-df79-34765e995df4', '2018-11-06 14:51:51.435716', '3c94f715-8663-4a40-a825-cc0d48c565d7', 'AD', 'AAABAD', '角色授权', '/Admin/Role/AuthenRoleMenuForm', '18', '3', null);
INSERT INTO `admin_menu` VALUES ('39e9fa00-5add-8e1d-cfa1-caee6e60ecf3', '2018-11-06 15:01:30.461389', null, 'AD', 'AD', '平台管理', '#', '4', '1', 'fa fa-laptop');
INSERT INTO `admin_menu` VALUES ('39e9fa05-ca2f-d67b-44cb-f8d320fa8147', '2018-11-06 15:07:26.639554', '39e9fa00-5add-8e1d-cfa1-caee6e60ecf3', 'AA', 'ADAA', '图片管理', '/Platform/OssFile/Index', '41', '2', null);
INSERT INTO `admin_menu` VALUES ('39e9fa66-d8ae-1ec5-1388-400f1a1bcd69', '2018-11-06 16:53:27.343262', '19549426-882b-461b-b73f-f7b9917b565e', 'AD', 'AAAD', '缓存配置', '/Admin/systemSetting/Index', '14', '2', 'fa fa-soundcloud');
INSERT INTO `admin_menu` VALUES ('3c94f715-8663-4a40-a825-cc0d48c565d7', '2018-02-10 00:00:00.000000', '19549426-882b-461b-b73f-f7b9917b565e', 'AB', 'AAAB', '角色管理', '/Admin/Role/Index', '12', '2', 'fa fa-binoculars');
INSERT INTO `admin_menu` VALUES ('5bed3d37-d4af-4376-aea3-1f1934d93121', '2018-02-10 00:00:00.000000', '3c94f715-8663-4a40-a825-cc0d48c565d7', 'AB', 'AAABAB', '修改角色', '/Admin/Role/From', '16', '3', null);
INSERT INTO `admin_menu` VALUES ('681b2847-a636-44ca-bab0-3d3636362df4', '2018-02-10 00:00:00.000000', '3c94f715-8663-4a40-a825-cc0d48c565d7', 'AA', 'AAABAA', '添加角色', '/Admin/Role/From', '15', '3', null);
INSERT INTO `admin_menu` VALUES ('8f39914c-0ecf-4bd2-aafc-e6368350857b', '2018-02-10 00:00:00.000000', 'a7865d53-3cb8-48c1-b906-66bb363c6af1', 'AB', 'AAAAAB', '修改菜单', '/Admin/Menu/From', '13', '3', null);
INSERT INTO `admin_menu` VALUES ('9d3aa3eb-d00a-4890-9d8a-1a46a7db92ec', '2018-02-10 00:00:00.000000', null, 'AB', 'AB', '日志查看', '#', '2', '1', 'fa fa-file-archive-o');
INSERT INTO `admin_menu` VALUES ('a7865d53-3cb8-48c1-b906-66bb363c6af1', '2018-02-10 00:00:00.000000', '19549426-882b-461b-b73f-f7b9917b565e', 'AA', 'AAAA', '菜单管理', '/Admin/Menu/Index', '11', '2', 'fa fa-map-signs');
INSERT INTO `admin_menu` VALUES ('b7db0df8-8fd6-4bda-8883-a5ab889c9a44', '2018-02-10 00:00:00.000000', '3c94f715-8663-4a40-a825-cc0d48c565d7', 'AC', 'AAABAC', '删除角色', '/Admin/Role/RemoveForm', '17', '3', null);
INSERT INTO `admin_menu` VALUES ('bb605b33-1102-42b6-be91-c5447d2a1862', '2018-02-10 00:00:00.000000', 'e0b080d8-f222-4aca-9599-3a1868276e31', 'AC', 'AAACAC', '删除用户', '/Admin/User/RemoveForm', '20', '3', null);
INSERT INTO `admin_menu` VALUES ('c6da5e90-f810-4fc1-92ad-ace90b951b2b', '2018-02-10 00:00:00.000000', 'a7865d53-3cb8-48c1-b906-66bb363c6af1', 'AA', 'AAAAAA', '添加菜单', '/Admin/Menu/From', '12', '3', null);
INSERT INTO `admin_menu` VALUES ('e0b080d8-f222-4aca-9599-3a1868276e31', '2018-02-10 00:00:00.000000', '19549426-882b-461b-b73f-f7b9917b565e', 'AC', 'AAAC', '用户管理', '/Admin/User/Index', '13', '2', 'fa fa-github-square');
INSERT INTO `admin_menu` VALUES ('e1ff8300-03a7-44e7-853e-31f1aa71b737', '2018-02-10 00:00:00.000000', '9d3aa3eb-d00a-4890-9d8a-1a46a7db92ec', 'AA', 'ABAA', '登录日志', '/Admin/SystemLog/Logins', '1', '2', 'fa fa-street-view');
INSERT INTO `admin_menu` VALUES ('e455eabe-789d-42e8-aa16-632dd6cc7a4c', '2018-02-10 00:00:00.000000', 'e58f47de-4649-4ae6-b8da-f868d527eac2', 'AB', 'AAADAB', '取消授权', '/Admin/User/CancelRight', '2', '3', null);
INSERT INTO `admin_menu` VALUES ('e58f47de-4649-4ae6-b8da-f868d527eac2', '2018-02-10 00:00:00.000000', 'e0b080d8-f222-4aca-9599-3a1868276e31', 'AD', 'AAAD', '设置角色', '/Admin/User/Authen', '5', '3', null);
INSERT INTO `admin_menu` VALUES ('f0f2e88f-51c8-49a9-86ff-6580d00e9c81', '2018-02-10 00:00:00.000000', 'e58f47de-4649-4ae6-b8da-f868d527eac2', 'AA', 'AAADAA', '授权', '/Admin/User/GiveRight', '1', '3', null);
INSERT INTO `admin_menu` VALUES ('f5ff43d2-5c1e-4530-9798-aa1276595a81', '2018-02-10 00:00:00.000000', 'e0b080d8-f222-4aca-9599-3a1868276e31', 'AA', 'AAACAA', '添加用户', '/Admin/User/From', '18', '3', null);
INSERT INTO `admin_menu` VALUES ('fe49d9a1-3f26-404d-bb2d-ce9937cf7b8f', '2018-02-10 00:00:00.000000', 'e0b080d8-f222-4aca-9599-3a1868276e31', 'AB', 'AAACAB', '修改用户', '/Admin/User/From', '19', '3', null);

-- ----------------------------
-- Table structure for admin_pageview
-- ----------------------------
DROP TABLE IF EXISTS `admin_pageview`;
CREATE TABLE `admin_pageview` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `user_id` char(36) DEFAULT NULL,
  `login_name` varchar(50) DEFAULT NULL,
  `url` varchar(100) DEFAULT NULL,
  `ip` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of admin_pageview
-- ----------------------------
INSERT INTO `admin_pageview` VALUES ('08d6592d-9954-9982-644e-fe6d8ac1452e', '2018-12-03 22:42:50.036209', 'f5cf28ca-d5c4-c12f-804f-08d614d69906', 'superadmin', '/Admin/SystemLog/Visits', '::1');
INSERT INTO `admin_pageview` VALUES ('08d6592d-9998-1b42-1a67-1bf7893daad6', '2018-12-03 22:42:50.478620', 'f5cf28ca-d5c4-c12f-804f-08d614d69906', 'superadmin', '/Admin/SystemLog/VisitsList', '::1');

-- ----------------------------
-- Table structure for admin_pathcode
-- ----------------------------
DROP TABLE IF EXISTS `admin_pathcode`;
CREATE TABLE `admin_pathcode` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `code` varchar(50) DEFAULT NULL,
  `len` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of admin_pathcode
-- ----------------------------

-- ----------------------------
-- Table structure for admin_role
-- ----------------------------
DROP TABLE IF EXISTS `admin_role`;
CREATE TABLE `admin_role` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of admin_role
-- ----------------------------
INSERT INTO `admin_role` VALUES ('66e1611d-92e0-4851-a965-a8b0cf50d97c', '0001-01-01 00:00:00.000000', '超级管理员', '超级管理员');
INSERT INTO `admin_role` VALUES ('a2e07549-4619-455d-93d5-c6b5193f7c1e', '0001-01-01 00:00:00.000000', 'guest', '游客');

-- ----------------------------
-- Table structure for admin_role_menu
-- ----------------------------
DROP TABLE IF EXISTS `admin_role_menu`;
CREATE TABLE `admin_role_menu` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `role_id` char(36) NOT NULL,
  `menu_id` char(36) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_admin_role_menu_menu_id` (`menu_id`) USING BTREE,
  KEY `IX_admin_role_menu_role_id` (`role_id`) USING BTREE,
  CONSTRAINT `admin_role_menu_ibfk_1` FOREIGN KEY (`menu_id`) REFERENCES `admin_menu` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `admin_role_menu_ibfk_2` FOREIGN KEY (`role_id`) REFERENCES `admin_role` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of admin_role_menu
-- ----------------------------
INSERT INTO `admin_role_menu` VALUES ('0d96e1a9-2a6d-490f-9f43-c724dc3bff21', '2018-02-10 00:00:00.000000', 'a2e07549-4619-455d-93d5-c6b5193f7c1e', 'e1ff8300-03a7-44e7-853e-31f1aa71b737');
INSERT INTO `admin_role_menu` VALUES ('1c1b13c6-57cd-4356-b96d-a908143bd647', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', '39028f62-460a-4522-a337-70978d00819b');
INSERT INTO `admin_role_menu` VALUES ('1f6951a8-30fc-41f6-bf86-7727cafc8a2a', '2018-02-10 00:00:00.000000', 'a2e07549-4619-455d-93d5-c6b5193f7c1e', '19549426-882b-461b-b73f-f7b9917b565e');
INSERT INTO `admin_role_menu` VALUES ('271a8c65-1942-4b12-8b47-c5b904dace79', '2018-02-10 00:00:00.000000', 'a2e07549-4619-455d-93d5-c6b5193f7c1e', '9d3aa3eb-d00a-4890-9d8a-1a46a7db92ec');
INSERT INTO `admin_role_menu` VALUES ('2f29899b-67da-4937-85bb-ef0179da54a9', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', '3c94f715-8663-4a40-a825-cc0d48c565d7');
INSERT INTO `admin_role_menu` VALUES ('37f842dc-116f-46e5-930c-286c792f05e4', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', '19549426-882b-461b-b73f-f7b9917b565e');
INSERT INTO `admin_role_menu` VALUES ('39e9fea2-7793-76d6-84bc-fba88285fe86', '2018-11-07 12:37:03.507523', '66e1611d-92e0-4851-a965-a8b0cf50d97c', '39e9fa05-ca2f-d67b-44cb-f8d320fa8147');
INSERT INTO `admin_role_menu` VALUES ('39e9fea2-7793-a1ad-ee0d-0c2cd87e2bd7', '2018-11-07 12:37:03.507523', '66e1611d-92e0-4851-a965-a8b0cf50d97c', '39e9fa00-5add-8e1d-cfa1-caee6e60ecf3');
INSERT INTO `admin_role_menu` VALUES ('3d0bf005-067c-4dee-8a74-e7ad3371492d', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'e58f47de-4649-4ae6-b8da-f868d527eac2');
INSERT INTO `admin_role_menu` VALUES ('47390799-e113-4549-985d-0488268f718d', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'e1ff8300-03a7-44e7-853e-31f1aa71b737');
INSERT INTO `admin_role_menu` VALUES ('4beea40b-8974-4ead-b136-4ecfa763266b', '2018-02-10 00:00:00.000000', 'a2e07549-4619-455d-93d5-c6b5193f7c1e', '3c94f715-8663-4a40-a825-cc0d48c565d7');
INSERT INTO `admin_role_menu` VALUES ('56feac04-dd90-4700-ad85-055670f640d4', '2018-02-10 00:00:00.000000', 'a2e07549-4619-455d-93d5-c6b5193f7c1e', '39028f62-460a-4522-a337-70978d00819b');
INSERT INTO `admin_role_menu` VALUES ('57aeba02-40c8-4203-87ab-ce2a71f61d13', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'e455eabe-789d-42e8-aa16-632dd6cc7a4c');
INSERT INTO `admin_role_menu` VALUES ('59369959-d78f-451b-99f4-acb7f4612522', '2018-02-10 00:00:00.000000', 'a2e07549-4619-455d-93d5-c6b5193f7c1e', 'a7865d53-3cb8-48c1-b906-66bb363c6af1');
INSERT INTO `admin_role_menu` VALUES ('6e73f2f8-519c-4d56-a204-8a7e70ba477d', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', '1fa642ab-f20b-44d2-a400-d19a1758f0ef');
INSERT INTO `admin_role_menu` VALUES ('7103031f-a6aa-46d5-ae0b-c63bda25ce7c', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', '681b2847-a636-44ca-bab0-3d3636362df4');
INSERT INTO `admin_role_menu` VALUES ('71ee3517-a63a-4dfc-8faf-d2f3be2aed05', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', '9d3aa3eb-d00a-4890-9d8a-1a46a7db92ec');
INSERT INTO `admin_role_menu` VALUES ('7389b65d-8f4e-4f8e-aedd-7482d4249224', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', '8f39914c-0ecf-4bd2-aafc-e6368350857b');
INSERT INTO `admin_role_menu` VALUES ('77260210-f402-453e-8b56-0d9fd7db9b52', '2018-02-10 00:00:00.000000', 'a2e07549-4619-455d-93d5-c6b5193f7c1e', 'e0b080d8-f222-4aca-9599-3a1868276e31');
INSERT INTO `admin_role_menu` VALUES ('7da5b59e-eaae-4308-a812-223e7fa4daa5', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'f0f2e88f-51c8-49a9-86ff-6580d00e9c81');
INSERT INTO `admin_role_menu` VALUES ('88278325-573c-4103-b73b-f8fea9873051', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'bb605b33-1102-42b6-be91-c5447d2a1862');
INSERT INTO `admin_role_menu` VALUES ('901a18d5-2f49-4932-bf28-e870b94bab4b', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', '5bed3d37-d4af-4376-aea3-1f1934d93121');
INSERT INTO `admin_role_menu` VALUES ('9ee7c3de-4d5f-402a-b87a-9d8a2ed48b91', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'a7865d53-3cb8-48c1-b906-66bb363c6af1');
INSERT INTO `admin_role_menu` VALUES ('b11dfa7c-5671-4be6-8116-5a52aeeebd53', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'c6da5e90-f810-4fc1-92ad-ace90b951b2b');
INSERT INTO `admin_role_menu` VALUES ('bbaf455b-d9e4-4b45-ba94-ccc5a0535736', '2018-02-10 00:00:00.000000', 'a2e07549-4619-455d-93d5-c6b5193f7c1e', 'e58f47de-4649-4ae6-b8da-f868d527eac2');
INSERT INTO `admin_role_menu` VALUES ('d870e40b-7b8c-4dfb-af91-eca3726b97d3', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'fe49d9a1-3f26-404d-bb2d-ce9937cf7b8f');
INSERT INTO `admin_role_menu` VALUES ('ea26fac6-3617-4465-aad0-9d833a24fe5e', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'e0b080d8-f222-4aca-9599-3a1868276e31');
INSERT INTO `admin_role_menu` VALUES ('ea779c68-b03f-41c3-bb88-890ea66bdb4e', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'f5ff43d2-5c1e-4530-9798-aa1276595a81');
INSERT INTO `admin_role_menu` VALUES ('ffdfec35-4227-471f-b877-a67b14725dad', '2018-02-10 00:00:00.000000', '66e1611d-92e0-4851-a965-a8b0cf50d97c', 'b7db0df8-8fd6-4bda-8883-a5ab889c9a44');

-- ----------------------------
-- Table structure for admin_user
-- ----------------------------
DROP TABLE IF EXISTS `admin_user`;
CREATE TABLE `admin_user` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `add_user_id` char(36) NOT NULL,
  `update_user_id` char(36) DEFAULT NULL,
  `update_time` datetime(6) DEFAULT NULL,
  `login_name` varchar(50) DEFAULT NULL,
  `real_name` varchar(50) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `password` varchar(100) DEFAULT NULL,
  `is_superMan` bit(1) NOT NULL,
  `is_delete` bit(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of admin_user
-- ----------------------------
INSERT INTO `admin_user` VALUES ('b7b468a4-36f6-465e-adb5-ede7b86e98a8', '2018-02-10 00:00:00.000000', '00000000-0000-0000-0000-000000000000', null, null, 'guest', '游客', 'wangboy91@qq.com', 'E10ADC3949BA59ABBE56E057F20F883E', '\0', '\0');
INSERT INTO `admin_user` VALUES ('f5cf28ca-d5c4-c12f-804f-08d614d69906', '2018-02-10 00:00:00.000000', '00000000-0000-0000-0000-000000000000', null, null, 'superadmin', '超级管理员', 'wangboy91@qq.com', 'E10ADC3949BA59ABBE56E057F20F883E', '', '\0');

-- ----------------------------
-- Table structure for admin_user_role
-- ----------------------------
DROP TABLE IF EXISTS `admin_user_role`;
CREATE TABLE `admin_user_role` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `user_id` char(36) NOT NULL,
  `role_id` char(36) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_admin_user_role_role_id` (`role_id`) USING BTREE,
  KEY `IX_admin_user_role_user_id` (`user_id`) USING BTREE,
  CONSTRAINT `admin_user_role_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `admin_role` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `admin_user_role_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `admin_user` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of admin_user_role
-- ----------------------------
INSERT INTO `admin_user_role` VALUES ('13f5a394-9d14-4e6c-84f1-8815a01e9317', '2018-02-10 00:00:00.000000', 'f5cf28ca-d5c4-c12f-804f-08d614d69906', '66e1611d-92e0-4851-a965-a8b0cf50d97c');
INSERT INTO `admin_user_role` VALUES ('416b9433-13d0-44bd-a4bf-80645eae94f2', '2018-02-10 00:00:00.000000', 'b7b468a4-36f6-465e-adb5-ede7b86e98a8', 'a2e07549-4619-455d-93d5-c6b5193f7c1e');

-- ----------------------------
-- Table structure for autohistory
-- ----------------------------
DROP TABLE IF EXISTS `autohistory`;
CREATE TABLE `autohistory` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RowId` varchar(50) NOT NULL,
  `TableName` varchar(128) NOT NULL,
  `Changed` longtext,
  `Kind` int(11) NOT NULL,
  `Created` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of autohistory
-- ----------------------------

-- ----------------------------
-- Table structure for oss_file
-- ----------------------------
DROP TABLE IF EXISTS `oss_file`;
CREATE TABLE `oss_file` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `add_user_id` char(36) NOT NULL,
  `update_user_id` char(36) DEFAULT NULL,
  `update_time` datetime(6) DEFAULT NULL,
  `folder_id` char(36) DEFAULT NULL,
  `save_path` varchar(500) NOT NULL,
  `file_name` varchar(50) NOT NULL,
  `file_type` int(11) NOT NULL,
  `describe` varchar(500) DEFAULT NULL,
  `file_size` int(11) NOT NULL,
  `is_delete` bit(1) NOT NULL,
  `file_extension` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of oss_file
-- ----------------------------

-- ----------------------------
-- Table structure for oss_folder
-- ----------------------------
DROP TABLE IF EXISTS `oss_folder`;
CREATE TABLE `oss_folder` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `add_user_id` char(36) NOT NULL,
  `update_user_id` char(36) DEFAULT NULL,
  `update_time` datetime(6) DEFAULT NULL,
  `name` varchar(500) NOT NULL,
  `code` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of oss_folder
-- ----------------------------

-- ----------------------------
-- Table structure for sys_setting
-- ----------------------------
DROP TABLE IF EXISTS `sys_setting`;
CREATE TABLE `sys_setting` (
  `Id` char(36) NOT NULL,
  `add_time` datetime(6) NOT NULL,
  `setting_key` varchar(100) NOT NULL,
  `setting_value` longtext,
  `data_type` varchar(100) NOT NULL,
  `description` varchar(500) DEFAULT NULL,
  `add_user_Id` char(36) DEFAULT NULL,
  `update_user_Id` char(36) DEFAULT NULL,
  `update_time` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of sys_setting
-- ----------------------------

-- ----------------------------
-- Table structure for __efmigrationshistory
-- ----------------------------
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------
-- Records of __efmigrationshistory
-- ----------------------------
INSERT INTO `__efmigrationshistory` VALUES ('20181125121153_init', '2.1.4-rtm-31024');
