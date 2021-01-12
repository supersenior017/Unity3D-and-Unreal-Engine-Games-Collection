/// The class includes function for restarting level, Score points. Get_Points is used to show the current Points in the game.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/GameModeBase.h"
#include "Kismet/GameplayStatics.h"
#include "UObject/Class.h"
#include "FFGameMode.generated.h"


UCLASS()
class FLAPPY_FLOYD_API AFFGameMode : public AGameModeBase
{
	GENERATED_BODY()
    
    
public:
    
    AFFGameMode();
    void Restart_Level();
    void Add_Point();
    UFUNCTION(BlueprintCallable, Category = "Score")
    float Get_Points();
    
    
//private:
    
    float Points;
	
};
