/// Fill out your copyright notice in the Description page of Project Settings.


#include "Flappy.h"
#include "Camera/CameraComponent.h"
#include "FFGameMode.h"
#include "Obstacles.h"
#include "Scoring_Line.h"

// Konstruktor
AFlappy::AFlappy()
{

    PrimaryActorTick.bCanEverTick = true;
    
    
    //root is object / <Type of this object>  / "given name"
    root = CreateDefaultSubobject<USceneComponent>(TEXT("Root_scene"));
    //default root component should be "root"
    RootComponent = root;
    
    
    Cam = CreateDefaultSubobject<UCameraComponent>(TEXT("Camera"));
    //Camera is child of root
    Cam->SetupAttachment(RootComponent);
    
    Circle = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("Circle"));
    //Circle is child of root
    Circle->SetupAttachment(RootComponent);

    

    //Circle should be default class to be possess when starting game
    AutoPossessPlayer = EAutoReceiveInput::Player0;
}

// Called when the game starts or when spawned
void AFlappy::BeginPlay()
{
	Super::BeginPlay();

     //overriding collisions function with selfmade func. OnCollisionsEnter should be called &referenz to it
    Circle->OnComponentHit.AddDynamic(this, &AFlappy::OnCollisionsEnter);
    Circle->OnComponentBeginOverlap.AddDynamic(this, & AFlappy::BeginOverlap);
    
    //Referenz to GameMode
    GM = Cast<AFFGameMode>(GetWorld()->GetAuthGameMode());
    
    //Get initial mass
    Weight = Circle->GetMass();
    
    Highscore = CreateWidget<UUserWidget>(GetWorld(), DefaultHUD);
    Highscore->AddToViewport();
	
}

// Called every frame
void AFlappy::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

    
    Delta_Backup = DeltaTime;
    Actual_Velocity = Circle->GetPhysicsLinearVelocity();
    
    //if velocity is positiv -> force down. Result is smoother.
    if(Actual_Velocity.Z > 0)
    {
        FVector Force_Down = FVector(0.0f, 0.0f, -Weight * 1000);
        Circle->AddForce(Force_Down);
    }
    
}


void AFlappy::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);
    
    
    //Project Settings -> space bar
    // call the Jump functon when "Space Bar" is clicked. Just Once.
    PlayerInputComponent->BindAction("Jump", IE_Pressed, this, &AFlappy::Jump);

}


void AFlappy::OnCollisionsEnter(UPrimitiveComponent * MyComp, AActor* Other, UPrimitiveComponent* OtherComp, FVector NormalImpulse, const FHitResult& Hit)
{
    // check other
    AObstacles* wall = Cast<AObstacles>(Other);
    
    //when successful/when exists -> end game
    if(wall)
    {
        Game_Off = true;
        //after a specific time -> cal TheEnd()
        GetWorldTimerManager().SetTimer(DeathTimer, this, &AFlappy::TheEnd, 2.0f, false);

    }
}



void AFlappy::BeginOverlap(UPrimitiveComponent* OverlappedComponent, AActor* OtherActor, UPrimitiveComponent* OtherComp, int32 OtherBodyIndex, bool bFromSweep, const FHitResult &SweepResult)
{

    AScoring_Line* scoring = Cast<AScoring_Line>(OtherActor);
    
    if(!Game_Off)
    {
        //when successful/when exists -> end game
        if(scoring)
        {
            GM->Add_Point();
        }
    }
}


void AFlappy::TheEnd()
{
    
    GM->Restart_Level();
    
}


// Jumping is allowed, when Flappy is alive. Velocity->zero (jump always the same height. Move up, when clicked. ForwardForce can be changed.
void AFlappy::Jump()
{
    if(!Game_Off)
       {
           Circle->SetPhysicsLinearVelocity(FVector(0.0f, 0.0f, 0.0f));
           FVector force = FVector(0.0f, 0.0f, Weight* ForwardForce);
           Circle->AddImpulse(force);
       }
}
