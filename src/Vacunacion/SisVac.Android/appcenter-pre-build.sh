#!/usr/bin/env bash

SPLASH_ACTIVITY="$BUILD_REPOSITORY_LOCALPATH/src/Vacunacion/SisVac.Android/SplashActivity.cs"
MANIFEST="$BUILD_REPOSITORY_LOCALPATH/src/Vacunacion/SisVac.Android/Properties/AndroidManifest.xml"
APPSETTINGS="$BUILD_REPOSITORY_LOCALPATH/src/Vacunacion/SisVac/appsettings.json"
VERSIONNAME=`grep versionName ${MANIFEST} | sed 's/.*versionName\s*=\s*\"\([^\"]*\)\".*/\1/g'`

sed -i.bak "s/android:versionName="\"${VERSIONNAME}\""/android:versionName="\"1.0.${APPCENTER_BUILD_ID}\""/" ${MANIFEST}
rm -f ${MANIFEST}.bak
sed -i.bak "s/android:versionCode="\"1\""/android:versionCode="\"${APPCENTER_BUILD_ID}\""/" ${MANIFEST}
rm -f ${MANIFEST}.bak

sed -i.bak "s/package="\"gob.ogtic.sisvac\""/package="\"${APP_BUNDLE_ID}\""/" ${MANIFEST}
rm -f ${MANIFEST}.bak

# Print out file for reference
cat $MANIFEST
echo "Updated manifest!"

sed -i.bak "s/Label = "\"SisVacRD\""/Label = "\"${APP_NAME}\""/" ${SPLASH_ACTIVITY}
rm -f ${SPLASH_ACTIVITY}.bak

cat $SPLASH_ACTIVITY
echo "Updated splash activity!"

echo "{ \"CitizenApiBaseUrl\": \"${CITIZEN_API_BASE_URL}\", \"CitizenApiKey\": \"${CITIZEN_API_KEY}\" }" > ${APPSETTINGS}
cat $APPSETTINGS
echo "Updated appsettings.json!"