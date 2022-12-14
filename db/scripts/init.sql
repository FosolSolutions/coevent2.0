PRINT 'Creating default database'
CREATE DATABASE $(DB_NAME);
GO
USE $(DB_NAME);
GO

PRINT 'Adding default admin account'
CREATE LOGIN $(DB_USER) WITH PASSWORD = '$(DB_PASSWORD)';
GO
CREATE USER $(DB_USER) FOR LOGIN $(DB_USER);
GO
ALTER SERVER ROLE sysadmin ADD MEMBER [$(DB_USER)];
GO

-- PRINT 'Adding keycloak database'
-- CREATE DATABASE $(AUTH_DB_NAME);
-- GO
