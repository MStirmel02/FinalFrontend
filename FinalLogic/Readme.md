Logic layer serves little purpose in most cases for this because of sepearation and compartmentalization.
Any exceptions occuring in data layer are handled by the backend API and decisions are made before the data is even recieved by the 
frontend, with only a rest call and deserialization occuring in the data access layer of the frontend. By the time the data reaches the logic layer,
the data is already formatted in such a way that exceptions should not occur, but try catches are still added in the event something was overlooked or missed.