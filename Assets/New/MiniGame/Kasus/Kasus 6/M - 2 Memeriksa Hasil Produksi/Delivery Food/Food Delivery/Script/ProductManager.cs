using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductManager : MonoBehaviour
{
    public int price = 100;

    //Number of ingredients this product consists.
    //must be between 1 to 6 item.
    public int totalIngredients;

    //In the inspector, use the number above "totalIngredients" as the lentgh of this array.
    //then assign proper ID of desired ingredients to array's childs.
    //note that IDs index should be carefully selected from existing ingrediets. in this kit we have 21 ingredients,
    //so we can choose any index from 0 to 20.
    //we also can use duplicate indexs. meaning a product can consist of two or more of the same ingredient.
    public int[] ingredientsIDs;
}
