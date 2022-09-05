#!/bin/bash

#this copies everything important but migrations

cp beon_clone_asp.csproj $1/
cp -r Components $1/
cp -r Controllers $1/
cp -r Data $1/
cp -r Hubs $1/
cp -r Infrastructure $1/
cp -r Models $1/
cp -r Services $1/
cp -r Views $1/
cp -r Properties $1/
cp -r wwwroot $1/
cp -r Validation $1/
cp Program.cs $1/
cp .gitignore $1/

