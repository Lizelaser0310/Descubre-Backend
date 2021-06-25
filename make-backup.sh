#!/bin/sh

read -p "version: " VERSION
echo "Making v${VERSION} backup..."
#mssql-scripter -S localhost -d HISCOM -U sa -P "_fIq}Q#1" --schema-and-data > "hiscom-v${VERSION}.sql"
docker build -t lizeth/make-backup .
docker rmi $(docker images --filter "dangling=true" -q --no-trunc) || true
docker run --rm -e VERSION=${VERSION} -v $(pwd):/script --network host lizeth/make-backup
sudo chown 1000:1000 "hiscom-v${VERSION}.sql"
echo "OK!"
