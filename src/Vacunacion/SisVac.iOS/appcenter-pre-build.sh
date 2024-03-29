#!/usr/bin/env bash

PLIST="$BUILD_REPOSITORY_LOCALPATH/src/Vacunacion/SisVac.iOS/Info.plist"
APPSETTINGS="$BUILD_REPOSITORY_LOCALPATH/src/Vacunacion/SisVac/appsettings.json"

/usr/libexec/PlistBuddy -c "Set :CFBundleShortVersionString 1.0.${APPCENTER_BUILD_ID}" $PLIST

cat $PLIST
echo "Updated info.plist!"

echo "{ \"CitizenApiBaseUrl\": \"${CITIZEN_API_BASE_URL}\", \"CitizenApiKey\": \"${CITIZEN_API_KEY}\" }" > ${APPSETTINGS}
cat $APPSETTINGS
echo "Updated appsettings.json!"