# Problem synopsis

Default struct equality is not optimistic for hash based operations

## Reference

https://devblogs.microsoft.com/premier-developer/performance-implications-of-default-struct-equality-in-c/

## More details

- If a struct does not provide Equal and GetHashCode overrides then the default versions of these methods from System.ValueType are used and they are reflection-based


### Problem 1: Boxing Allocation

- The way the CLR is designed, every call to a member defined in System.ValueType or System.Enum types cause a boxing allocation

### Problem 2: Reflection based implementation is slow

- Reflection is a powerful tool when used correctly. But it is horrible if it’s used on an application’s hot path.


### Problem 3: Hash collision

- Ideally the GetHashCode should combile the hash codes of all the fields
- But the only way to get a hash code of a field in a ValueType method is to use reflection
- So based on point 2, the CLR authors decided to trade speed over the distribution and the default GetHashCode version just returns a hash code of a first non-null field

1. If the first field is always the same, the default hash function returns the same value for all the elements
2. This effectively transforms a hash set into a linked list with O(N) for insertion and lookup operations
3. And the operation that populates the collection becomes O(N^2) (N insertions with O(N) complexity per insertion). and that uses reflection under the hood!


### Benchmarking by
https://benchmarkdotnet.org/articles/guides/getting-started.html

