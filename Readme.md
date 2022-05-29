
#### Design of In memory object
An in memory object is used to store activities. 
There are two objects that serve this purpose:
- a dictionary of key, and list of values and timestamps against it
- a dictionary of key, and running sum for the current window


#### Save in O(1)

On every save, the item is added to the first dictionary, and in a fire and forget mechanism, after returning data to the user, the sums are updated in the background in the second object in memory.

#### Get in O(1)

Gets are done against the second dictionary.

For getting, the running sum in the current 12 hour window, a pre-calculated set of sum is kept in memory.

To achieve a lookup of O(1), the running sum against a key, is set in memory. Think of it more as a sort of ETL, that is run for every prune window.
This is done as a sceduler, which runs after every cycle of 12 hour window, and removes older data.

#### Test Coverage

The test coverage is 100%, except the tool shows 87% now due to missing test against startup file. This file need not be tested, but skipping on finding how to remove this from coverage for now.
