using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// thx to this guy    https://medium.com/@kunaltandon.kt/creating-a-dotted-line-in-unity-ca044d02c3e2
///
/// draw dotted lines
/// </summary>



    public class Dot_lines: MonoBehaviour
    {
        // Inspector fields
        public Sprite Dot;
        [Range(0.01f, 1f)]
        public float Size;
        [Range(0.1f, 2f)]
        public float Delta;

        //Static Property with backing field
        private static Dot_lines instance;
        public static Dot_lines Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<Dot_lines>();
                return instance;
            }
        }

        //Utility fields
        List<Vector2> positions = new List<Vector2>();
        List<GameObject> dots = new List<GameObject>();

        // Update is called once per frame

      
    void FixedUpdate()
        {
        
        if (positions.Count > 0)
            {
                DestroyAllDots();
                positions.Clear();
            }

        }
        

    
        
    
        private void DestroyAllDots()
        {
            foreach (var dot in dots)
            {
                Destroy(dot);
            }
            dots.Clear();
        }

        GameObject GetOneDot()
        {
            var gameObject = new GameObject();
            gameObject.transform.localScale = Vector3.one * Size;
            gameObject.transform.parent = transform;

            var sr = gameObject.AddComponent<SpriteRenderer>();
            sr.sprite = Dot;
            return gameObject;
        }

        public void DrawDottedLine(Vector2 start, Vector2 end)
        {
            DestroyAllDots();

            Vector2 point = start;
        Vector2 direction = (end - start).normalized;

            while ((end - start).magnitude > (point - start).magnitude)
            {
                positions.Add(point);
                point += (direction * Delta);
            }

        
            Render();
        }

        private void Render()
        {
            foreach (var position in positions)
            {
                var g = GetOneDot();
                g.transform.position = position;
                dots.Add(g);
            }

     
  
        }
    }

