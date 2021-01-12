/// This function controlls Flappy the circle. When hit on a wall (onCollisionenter) -> game over. When it goes through the air -> point++ (Beginoverlap). Camera  Jumping are added.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Pawn.h"
#include "Blueprint/UserWidget.h"
#include "Flappy.generated.h"


UCLASS()
class FLAPPY_FLOYD_API AFlappy : public APawn
{
	GENERATED_BODY()

public:
	// Sets default values for this pawn's properties
	AFlappy();
    

    virtual void Tick(float DeltaTime) override;

    // Called to bind functionality to input
    virtual void SetupPlayerInputComponent(class UInputComponent* PlayerInputComponent) override;

    //access to child class only
protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;

    
    
    // We do not want the camera follow flappy. We need a static root scene and camera is a child of it
    //Uproperty function: visibil everywhere and showing details in Blue rints

    UPROPERTY(VisibleAnywhere, BlueprintReadOnly);
    USceneComponent* root;
    
    UPROPERTY(VisibleAnywhere, BlueprintReadOnly);
    UStaticMeshComponent* Circle;
    
    UPROPERTY(VisibleAnywhere, BlueprintReadOnly);
    class UCameraComponent* Cam;
    
    UPROPERTY(EditAnywhere, BlueprintReadOnly, Category = "UMG");
    TSubclassOf<UUserWidget>DefaultHUD;

    UUserWidget* Highscore;

    
    //variables
    
    class AFFGameMode* GM;
    bool Game_Off;
    float Weight;
    UPROPERTY(EditAnywhere, Category = "Flappy_Parameter");
    float ForwardForce;
    //save the local variable DeltaTime
    float Delta_Backup;
    
    //Vector3 for saving actual velocity
    FVector Actual_Velocity;
    
    //Wait-Timer after Flappy dies
    FTimerHandle DeathTimer;
    

    //Collisiondetection, when actors bump together. Taking from Unreal Engine Docs: AActor::NotifyHit
    UFUNCTION()
    void OnCollisionsEnter(UPrimitiveComponent * MyComp, AActor* Other, UPrimitiveComponent* OtherComp, FVector NormalImpulse, const FHitResult& Hit);



    // using example from 
    //https://forums.unrealengine.com/development-discussion/c-gameplay-programming/106815-how-to-use-onactorbeginoverlap-c-code
    
    UFUNCTION()
    void BeginOverlap(UPrimitiveComponent* OverlappedComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult &SweepResult);

    //game ends
    void TheEnd();
    
    //Flappy jumps
    void Jump();

};
