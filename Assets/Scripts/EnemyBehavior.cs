using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
	[SerializeField]
	int gameOverScene = 0;

	[SerializeField]
	Color consumableColor;

	[SerializeField]
	Color chaseColor;

	int level;

	[SerializeField]
	int chanceToBeConsumable;

	[SerializeField]
	int minLevelAbove = 1;

	[SerializeField]
	int maxLevelAbove = 3;

	[SerializeField]
	float minMovementSpeed = 4f;

	[SerializeField]
	float maxMovementSpeed = 6f;

	[SerializeField]
	float switchMovementTime = 5f;

	[SerializeField]
	float acceptableOffset = 0.5f;

	[SerializeField]
	float notCollidableOnStartTime = 1f;

	bool horizontal;

	Transform player;

	float timer;
	float moveSpeed;
	Rigidbody2D rb;
	bool canMove = false;
	private Vector3 moveDirection = Vector3.zero;

	[Header("Clamp Settings")]
    public Vector2 minClamp;
    public Vector2 maxClamp;

	void setMovement(){
		int horizontalChoice = Random.Range(0,2);
		int directionChoice = Random.Range(0, 4);
		moveSpeed = Random.Range(minMovementSpeed, maxMovementSpeed);
		timer = 0f;
		if(horizontalChoice == 0){
			horizontal = true;
		}
		if(horizontalChoice == 1){
			horizontal = false;
		}
		moveDirection = Vector3.zero;
		if(level < PlayerLevel.playerLevel){
			if(directionChoice == 0) {
				moveDirection = Vector3.up;
			}
			if(directionChoice == 1){
				moveDirection = Vector3.down;
			}
			if(directionChoice == 2){
				moveDirection = Vector3.left;
			}
			if(directionChoice == 3){
				moveDirection = Vector3.right;
			}
		}

	}

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		setMovement();
		player = GameObject.FindGameObjectWithTag("Player").transform;

		int enemyTypeChoice = Random.Range(0, 100);
		if(enemyTypeChoice < chanceToBeConsumable){
			level = 1;
			return;
		}
		level = PlayerLevel.playerLevel + Random.Range(minLevelAbove, maxLevelAbove+1);
		GetComponent<CircleCollider2D>().enabled = false;
	}

	void HandleDirection()
    {
        if(horizontal || Mathf.Abs(player.position.y - transform.position.y) < acceptableOffset){
        	if(player.position.x - transform.position.x > acceptableOffset){
        		moveDirection = Vector3.right;
        		return;
        	}
        	if(transform.position.x - player.position.x > acceptableOffset){
        		moveDirection = Vector3.left;
        		return;
        	}
        }

        if(!horizontal || Mathf.Abs(player.position.x - transform.position.x) < acceptableOffset){
        	if(player.position.y - transform.position.y > acceptableOffset){
        		moveDirection = Vector3.up;
        		return;
        	}
        	if(transform.position.y - player.position.y > acceptableOffset){
        		moveDirection = Vector3.down;
        		return;
        	}
        }
    }

    void OnTriggerEnter2D(Collider2D other){
    	if(other.gameObject.CompareTag("Player")){
    		if(level <= PlayerLevel.playerLevel){
    			PlayerLevel.playerLevel = PlayerLevel.playerLevel + 1;
    			GetComponent<CircleCollider2D>().enabled = false;
    			Destroy(gameObject, 0.1f);
    		}
    		if(level>PlayerLevel.playerLevel){
    			SceneManager.LoadScene(gameOverScene);
    		}
    	}
    }

    // Update is called once per frame
    void Update()
    {
    	if(PlayerLevel.playerLevel < level){
    		GetComponent<SpriteRenderer>().color = chaseColor;
    	}
    	if(PlayerLevel.playerLevel >= level){
    		GetComponent<SpriteRenderer>().color = consumableColor;
    	}
        timer+=Time.deltaTime;
        if(timer >= notCollidableOnStartTime && !canMove){
        	GetComponent<CircleCollider2D>().enabled = true;
        	canMove = true;
        }
        if(timer >= switchMovementTime){
        	setMovement();
        }
        if(PlayerLevel.playerLevel <= level){
           	HandleDirection();
        }
    }

    void FixedUpdate(){
    	if(canMove){
    		rb.linearVelocity = moveDirection * moveSpeed;
    	}
    	transform.position = new Vector2(Mathf.Clamp(transform.position.x, minClamp.x, maxClamp.x), Mathf.Clamp(transform.position.y, minClamp.y, maxClamp.y));
    }
}