#!/usr/bin/env bash

PLIST=$BUILD_REPOSITORY_LOCALPATH//src/Vacunacion/SisVac.iOS/Info.plist
/usr/libexec/PlistBuddy -c "Set :CFBundleShortVersionString 1.0.${APPCENTER_BUILD_ID}" $PLIST

sed -i.bak "s/\"CitizenApiBaseUrl\": "\"\""/\"CitizenApiBaseUrl\": "\"${CITIZEN_API_BASE_URL}\""/" ${APPSETTINGS}
rm -f ${APPSETTINGS}.bak

cat $PLIST
echo "Updated info.plist!"

cat $APPSETTINGS
echo "Updated appsettings.json!"