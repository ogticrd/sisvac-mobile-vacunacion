#!/usr/bin/env bash

MANIFEST="$BUILD_REPOSITORY_LOCALPATH/src/Vacunacion/SisVac.Android/Properties/AndroidManifest.xml"
APPSETTINGS="$BUILD_REPOSITORY_LOCALPATH/src/Vacunacion/SisVac.Android/appsettings.json"

VERSIONNAME=`grep versionName ${MANIFEST} | sed 's/.*versionName\s*=\s*\"\([^\"]*\)\".*/\1/g'`

sed -i.bak "s/android:versionName="\"${VERSIONNAME}\""/android:versionName="\"1.0.${APPCENTER_BUILD_ID}\""/" ${MANIFEST}
rm -f ${MANIFEST}.bak
sed -i.bak "s/android:versionCode="\"1\""/android:versionCode="\"${APPCENTER_BUILD_ID}\""/" ${MANIFEST}
rm -f ${MANIFEST}.bak


sed -i.bak "s/\"CitizenApiBaseUrl\": "\"\""/\"CitizenApiBaseUrl\": "\"${CITIZEN_API_BASE_URL}\""/" ${APPSETTINGS}
rm -f ${APPSETTINGS}.bak

# Print out file for reference
cat $MANIFEST
cat $APPSETTINGS

echo "Updated manifest!"