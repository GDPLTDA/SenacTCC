#!/bin/bash
echo Iniciando Tests...

if cd pathfinder; then 
    git pull; 
    cd ..;
else 
    git clone https://github.com/GDPLTDA/Pathfinder.net.git pathfinder; 
fi
dotnet restore ./pathfinder/Source/
dotnet build ./pathfinder/Source/

PF="dotnet ./pathfinder/Source/src/Pathfinder.CLI/bin/Debug/netcoreapp1.1/PF.dll"
declare -a arr=("Maps100" "Maps100D2" "Maps100D2P" "Maps100P" "Maps20" "Maps20D2" "Maps20D2P" "Maps20P")

echo Limpando pastas...
for pasta in "${arr[@]}"
do
   echo "$pasta"
   rm -rf $pasta
done

printf "\n20x20/Random/Never...\n"
eval $PF genmap -l Maps20 -w 20 -h 20 -n 100
printf "\n100x100/Random/Never...\n"
eval $PF genmap -l Maps100 -w 100 -h 100 -n 100
printf "\n20x20/Random/IfAtMostOneObstacle...\n"
eval $PF genmap -l Maps20D2 -w 20 -h 20 -n 100 -d 2
printf "\n100x100/Random/IfAtMostOneObstacle...\n"
eval $PF genmap -l Maps100D2 -w 100 -h 100 -n 100 -d 2
printf "\n20x20/Pattern/Never...\n"
eval $PF genmap -l Maps20P -w 20 -h 20 -n 100 -p 2
printf "\n100x100/Pattern/Never...\n"
eval $PF genmap -l Maps100P -w 100 -h 100 -n 100  -p 10
printf "\n20x20/Pattern/IfAtMostOneObstacle...\n"
eval $PF genmap -l Maps20D2P -w 20 -h 20 -n 100 -d 2 -p 2
printf "\n100x100/Pattern/IfAtMostOneObstacle...\n"
eval $PF genmap -l Maps100D2P -w 100 -h 100 -n 100 -d 2  -p 10

BAT="${PF} batch -a 0 1 3 4 -h 0 1 2 3 -m 2 5 -c 1 2 -f 0 1 2 -l "
for pasta in "${arr[@]}"
do
    eval "$BAT $pasta"
done


