## Music API Documentation

This project provides a backend API for managing songs, artists, and albums in a music application. 
It enables creating new entities, retrieving lists and details, and performing searches based on specific criteria.

### Albums Controller

This controller is responsible for managing albums in the Music API.

#### API Endpoints

- **POST /api/albums**

  Creates a new album.

  - Request Body: `multipart/form-data`
    - `name` (string, required): The name of the album.
    - `image` (file, optional): The cover image file for the album.

  - Response:
    - Status: 201 Created
    - Content: None

- **GET /api/albums**

  Retrieves all albums.

  - Request Body: None

  - Response:
    - Status: 200 OK
    - Content: JSON array of album objects
      - `id` (integer): The unique identifier of the album.
      - `name` (string): The name of the album.
      - `imageUrl` (string): The URL of the album cover image.

- **GET /api/albums/albumdetails?albumId={albumId}**

  Retrieves the details of a specific album.

  - Request Parameters:
    - `albumId` (integer, required): The ID of the album.

  - Response:
    - Status: 200 OK
    - Content: JSON object representing the album details
      - `id` (integer): The unique identifier of the album.
      - `name` (string): The name of the album.
      - `imageUrl` (string): The URL of the album cover image.
      - `songs` (array): Array of song objects associated with the album
        - `id` (integer): The unique identifier of the song.
        - ... (other song properties)

### Artists Controller

This controller is responsible for managing artists in the Music API.

#### API Endpoints

- **POST /api/artists**

  Creates a new artist.

  - Request Body: `multipart/form-data`
    - `name` (string, required): The name of the artist.
    - `image` (file, optional): The artist image file.

  - Response:
    - Status: 201 Created
    - Content: None

- **GET /api/artists**

  Retrieves a list of artists.

  - Request Parameters:
    - `pageNumber` (integer, optional): The page number for pagination. Default value is 1.
    - `pageSize` (integer, optional): The number of artists to include per page. Default value is 1.

  - Response:
    - Status: 200 OK
    - Content: JSON array of artist objects
      - `id` (integer): The unique identifier of the artist.
      - `name` (string): The name of the artist.
      - `imageUrl` (string): The URL of the artist image.

- **GET /api/artists/artistdetails?artistId={artistId}**

  Retrieves the details of a specific artist.

  - Request Parameters:
    - `artistId` (integer, required): The ID of the artist.

  - Response:
    - Status: 200 OK
    - Content: JSON object representing the artist details
      - `id` (integer): The unique identifier of the artist.
      - `name` (string): The name of the artist.
      - `imageUrl` (string): The URL of the artist image.
      - `songs` (array): Array of song objects associated with the artist
        - `id` (integer): The unique identifier of the song.
        - ... (other song properties)

### Songs Controller

This controller is responsible for managing songs in the Music API.

#### API Endpoints

- **POST /api/songs**

  Creates a new song.

  - Request Body: `multipart/form-data`
    - `title` (string, required): The title of the song.
    - `image` (file, optional): The song image file.
    - `audioFile` (file, optional): The audio file of the song.

  - Response:
    - Status: 201 Created
    - Content: None

- **GET /api/songs**

  Retrieves a paginated list of songs.

  - Request Parameters:
    - `pageNumber` (integer, optional): The page number for pagination. Default value is 1.
    - `pageSize` (integer, optional): The number of songs to include per page. Default value is 2.

  - Response:
    - Status: 200 OK
    - Content: JSON array of song objects
      - `id` (integer): The unique identifier of the song.
      - `title` (string): The title of the song.
      - `duration` (string): The duration of the song.
      - `imageUrl` (string): The URL of the song image.
      - `audioUrl` (string): The URL of the song audio.

- **GET /api/songs/featuredsongs**

  Retrieves a list of featured songs.

  - Response:
    - Status: 200 OK
    - Content: JSON array of song objects
      - `id` (integer): The unique identifier of the song.
      - `title` (string): The title of the song.
      - `duration` (string): The duration of the song.
      - `imageUrl` (string): The URL of the song image.
      - `audioUrl` (string): The URL of the song audio.

- **GET /api/songs/newsongs**

  Retrieves a list of new songs.

  - Response:
    - Status: 200 OK
    - Content: JSON array of song objects
      - `id` (integer): The unique identifier of the song.
      - `title` (string): The title of the song.
      - `duration` (string): The duration of the song.
      - `imageUrl` (string): The URL of the song image.
      - `audioUrl` (string): The URL of the song audio.

- **GET /api/songs/searchsongs?query={query}**

  Searches for songs based on a query.

  - Request Parameters:
    - `query` (string, required): The search query.

  - Response:
    - Status: 200 OK
    - Content: JSON array of song objects
      - `id` (integer): The unique identifier of the song.
      - `title` (string): The title of the song.
      - `duration` (string): The duration of the song.
      - `imageUrl` (string): The URL of the song image.
      - `audioUrl` (string): The URL of the song audio.

Please note that the API endpoints require authentication and authorization to access.
