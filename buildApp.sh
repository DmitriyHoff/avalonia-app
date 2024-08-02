#!/bin/bash
#parent_path=$( cd "$(dirname "${BASH_SOURCE[0]}")" ; pwd -P )
parent_path=$(dirname -- "$(readlink -f -- "$BASH_SOURCE")")
APP_NAME="dist/AvaloniaApp.app"
PUBLISH_OUTPUT_DIRECTORY="bin/Release/net8.0/publish/."

INFO_PLIST="macosDeploy/info.plist"
ICON_FILE="macosDeploy/AppIcon.icns"

ENTITLEMENTS="macosDeploy/AppEntitlements.entitlements"
#SIGNING_IDENTITY="Developer ID: Company Name" # matches Keychain Access certificate name

cd "$parent_path"
pwd
if [ -d "$APP_NAME" ]; then
    rm -rf "$APP_NAME"
fi

mkdir -p "$APP_NAME"

mkdir "$APP_NAME/Contents"
mkdir "$APP_NAME/Contents/MacOS"
mkdir "$APP_NAME/Contents/Resources"

cp "$INFO_PLIST" "$APP_NAME/Contents/Info.plist" &
cp "$ICON_FILE" "$APP_NAME/Contents/Resources/AppIcon.icns" &
cp -a "$PUBLISH_OUTPUT_DIRECTORY" "$APP_NAME/Contents/MacOS" &

codesign --force --deep --timestamp --options=runtime --entitlements "$ENTITLEMENTS" -s - "$APP_NAME" &
codesign --verify --verbose "$APP_NAME"
codesign -dv "$APP_NAME"