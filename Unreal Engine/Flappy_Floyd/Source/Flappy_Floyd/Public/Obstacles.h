/// obstacles move to left. After 2 Points -> Sinus movement of the obstacles. They will destroy themself after a distance.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "FFGameMode.h"
#include "Obstacles.generated.h"



UCLASS()
class FLAPPY_FLOYD_API AObstacles : public AActor
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	AObstacles();
    

protected:

	virtual void BeginPlay() override;
    
    
    UPROPERTY(VisibleAnywhere, BlueprintReadOnly);
    UStaticMeshComponent* Rectangle;
    
    //referenz GameMode for Score
    class AFFGameMode* GM;
    
    float RunningTime;


public:	

	virtual void Tick(float DeltaTime) override;

};
