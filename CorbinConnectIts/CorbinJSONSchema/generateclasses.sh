#!/usr/bin/env bash

# This is not being included in the build as the files are not going to change much if every and as
# the generated files will be simple checked in. This is more about docuementing the process to 
# generate the files should the need every arise.

dotnet publish GenerateClasses/GenerateClasses.sln -o ./bin

function generateclasses {
    echo "Generating $1 ..."
    dotnet bin/GenerateClasses.dll -f $2 -n $3 -c $1 -o "../CorbinConnectIts.Dto/${1}.cs"
}

generateclasses Configuration configuration.json "Econolite.Ode.CorbinConnectIts.Dto"
generateclasses PeriodicStatus periodicstatus.json "Econolite.Ode.CorbinConnectIts.Dto"
generateclasses CameraManualTriggerEvent cameramanualtrggerevent.json "Econolite.Ode.CorbinConnectIts.Dto"
generateclasses WrongWayEvent wrongwayevent.json  "Econolite.Ode.CorbinConnectIts.Dto"

unset -f generateclasses