

using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SparseArray<T> : IEnumerable<T>{
    private List<int> keys = new List<int>();
    private List<T> values = new List<T>();

    public void add(int key, T value){
        keys.Add(key);
        values.Add(value);
    }

    public T get(int key){
        if (keys.Contains(key)){
            return values[keys.IndexOf(key)];
        }
        return default (T);
    }

    public T getRandomElement(){
        if (values.Count == 0) return default(T);
        return values[Random.Range(0,values.Count)];
    }

    public IEnumerator<T> GetEnumerator(){
        return values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator(){
        return GetEnumerator();
    }

    public int Size(){
        return values.Count;
    }
}
