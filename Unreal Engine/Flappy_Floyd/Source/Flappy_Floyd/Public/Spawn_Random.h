/// Spawn() spawns a wall with random height. "Spawn_Now" (which spawns) is the Blueprint, which loads 3 Actors (above wall, below wall and the Score box).


#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Pawn.h"
#include "Spawn_Random.generated.h"

UCLASS()
class FLAPPY_FLOYD_API ASpawn_Random : public APawn
{
	GENERATED_BODY()

public:
	// Sets default values for this pawn's properties
	ASpawn_Random();
    
    
    UPROPERTY(EditAnywhere)
    TSubclassOf<class AActor> Spawn_Now;

    UFUNCTION(BlueprintCallable)
    void Spawn();

protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;

public:	
	// Called every frame
	virtual void Tick(float DeltaTime) override;

	// Called to bind functionality to input
	virtual void SetupPlayerInputComponent(class UInputComponent* PlayerInputComponent) override;

};
