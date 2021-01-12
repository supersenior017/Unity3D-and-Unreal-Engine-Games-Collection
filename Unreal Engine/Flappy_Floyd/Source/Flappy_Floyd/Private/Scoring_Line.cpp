/// Fill out your copyright notice in the Description page of Project Settings.


#include "Scoring_Line.h"


// Sets default values
AScoring_Line::AScoring_Line()
{

	PrimaryActorTick.bCanEverTick = true;
    
    
    //boxComponent is not visible
    Score = CreateDefaultSubobject<UBoxComponent>(TEXT("Score"));
    //Score is root
    RootComponent = Score;

}


void AScoring_Line::BeginPlay()
{
	Super::BeginPlay();
    
    //Referenz to GameMode
    GM = Cast<AFFGameMode>(GetWorld()->GetAuthGameMode());
	
}


void AScoring_Line::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
    
    //move obstacles to the left. value should be outsourced
    FVector pos = GetActorLocation();
    pos.X -= 10;
    SetActorLocation(pos);
    
    //after 2 points -> shaking with sinus. Speed could be increased over number of points
    if(GM->Points > 2)
    {
        float DeltaHeight = (FMath::Sin(RunningTime + DeltaTime) - FMath::Sin(RunningTime));
        pos.Z += DeltaHeight * 20; // Scale our height by a factor
        RunningTime += DeltaTime;
        SetActorLocation(pos);
        
    }
    
    //destroy scoring line, when far enough. Better: out of camera -> Destory(this)
    if(pos.X < -400)
    {
        Destroy(this);
    }

}

