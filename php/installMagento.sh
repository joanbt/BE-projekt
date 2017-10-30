ENV_VARIABLES=".env"

/var/www/html/bin/magento setup:install \
    --db-host=mysql \
    --db-name=${DATABASE_NAME} \
    --db-user=${DATABASE_USER} \
    --db-password=${DATABASE_PASSWORD} \
    --backend-frontname=${BACKEND_FRONTNAME} \
    --language=${LANGUAGE} \
    --timezone=${TIMEZONE} \
    --currency=${CURRENCY} \
    --admin-email=${ADMIN_EMAIL} \
    --admin-user=${ADMIN_USERNAME} \
    --admin-firstname=${ADMIN_FIRSTNAME} \
    --admin-lastname=${ADMIN_LASTNAME} \
    --admin-password=${ADMIN_PASSWORD} \
    --cleanup-database 
