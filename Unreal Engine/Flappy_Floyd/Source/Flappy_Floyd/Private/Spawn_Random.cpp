
#include "Spawn_Random.h"


ASpawn_Random::ASpawn_Random()
{
	PrimaryActorTick.bCanEverTick = false;

}


void ASpawn_Random::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void ASpawn_Random::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
    
    
}

// Called to bind functionality to input
void ASpawn_Random::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);
    
}

void ASpawn_Random::Spawn()
{
    if(Spawn_Now)
    {
        UWorld* world = GetWorld();
        
        if(world)
        {
            FActorSpawnParameters parameter;
            parameter.Owner = this;
            FRotator rotator;
            
            //spawn position, Values should be outsourced (max = 280; min = -20)
            FVector SpawnVector = FVector(600, 0.0f, FMath::RandRange(-20, 280));
            
            //spawn now
            world->SpawnActor<AActor>(Spawn_Now, SpawnVector, rotator, parameter);
        }
    }
}



