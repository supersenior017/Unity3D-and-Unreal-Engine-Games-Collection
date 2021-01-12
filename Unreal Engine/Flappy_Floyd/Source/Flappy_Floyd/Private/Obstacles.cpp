/// Fill out your copyright notice in the Description page of Project Settings.


#include "Obstacles.h"

// Sets default values
AObstacles::AObstacles()
{

	PrimaryActorTick.bCanEverTick = true;
    
    
    Rectangle= CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Rectangle"));
    //Rectangle is root
    RootComponent = Rectangle;
    
}

void AObstacles::BeginPlay()
{
	Super::BeginPlay();
    
    //Referenz to GameMode
    GM = Cast<AFFGameMode>(GetWorld()->GetAuthGameMode());
	
}


void AObstacles::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
    
    //move obstacles to the left. value should be outsourced
    FVector pos = GetActorLocation();
    pos.X -= 10;
    SetActorLocation(pos);
    
    //after 2 points -> shaking with sinus. Speed could be increased over number of points
    //https://answers.unrealengine.com/questions/434890/unreal-engine-beginner-fmathsin.html
    
    if(GM->Points > 2)
    {
        float DeltaHeight = (FMath::Sin(RunningTime + DeltaTime) - FMath::Sin(RunningTime));
        pos.Z += DeltaHeight * 20; // Scale our height by a factor
        RunningTime += DeltaTime;
        SetActorLocation(pos);
        
    }
    
    //destroy obstacle, when far enough. Better: out of camera -> Destory(this)
    if(pos.X < -400)
    {
        Destroy(this);
    }

}



