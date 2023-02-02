using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryController : MonoBehaviour
{
    public static categories currentCategory=categories.all;

}

public enum categories{
    child,adult,interesting,all
}
