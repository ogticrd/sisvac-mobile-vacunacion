#!/usr/bin/env bash

PLIST=$BUILD_REPOSITORY_LOCALPATH//src/Vacunacion/SisVac.iOS/Info.plist
/usr/libexec/PlistBuddy -c "Set :CFBundleShortVersionString 1.0.${APPCENTER_BUILD_ID}" $PLIST

cat $PLIST
echo "Updated info.plist!"

echo "{ \"CitizenApiBaseUrl\": \"${CITIZEN_API_BASE_URL}\" }" > ${APPSETTINGS}
cat $APPSETTINGS
echo "Updated appsettings.json!"