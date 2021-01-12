/// scoring line move to left. After 2 Points -> Sinus movement of the obstacles. They will destroy themself after a distance.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "Components/BoxComponent.h"
#include "FFGameMode.h"
#include "Scoring_Line.generated.h"

UCLASS()
class FLAPPY_FLOYD_API AScoring_Line : public AActor
{
	GENERATED_BODY()
	
public:	
	// Sets default values for this actor's properties
	AScoring_Line();

protected:

	virtual void BeginPlay() override;
    
    UPROPERTY(VisibleAnywhere, BlueprintReadOnly);
    UBoxComponent* Score;
    
    //referenz GameMode for Score
    class AFFGameMode* GM;
    
    float RunningTime;

public:	

	virtual void Tick(float DeltaTime) override;

};
