/// Fill out your copyright notice in the Description page of Project Settings.


#include "FFGameMode.h"


AFFGameMode::AFFGameMode()
{
    //reset Points
    Points = 0;

}

void AFFGameMode::Restart_Level()
{
    //Get current level name, convert return value from string to name value and pass it to the open level function. https://www.unrealidiot.com/how-to-reload-current-level-in-unreal-engine-4

    UGameplayStatics::OpenLevel(this, FName(*GetWorld()->GetName()), false);
    
}



void AFFGameMode::Add_Point()
{
    Points = Points+1;
    
    UE_LOG(LogTemp, Warning, TEXT("Text, %f"), Points);
}

float AFFGameMode::Get_Points()
{
    return Points;
}
