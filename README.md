# beon_clone_asp

Social media website

# Working features

* User accounts
  * nickname (display name) changing
  * password changing
  * avatar upload (S3 and Disk storage supported)
  * Friend list
  * Diary
  * user info page
  * Admin account (admin role)
    * Doesn't have any special permissions yet
* Home page
  * Displays the newest created topics
  * List of all users widget thing
* User Diary
  * diary entry creation
    * read/comment access restriction
      * Everyone, users, friends, no one
    * Post editing
* Comments
  * Realtime commenting (SignalR)
  * anonymous commenting
  * new comments notification
* Post body
  * BBCode
    * image insertion
    * simple tags
  * link parsing (breaks sometimes it seems)
  * smileyfaces
* Post deletion
* Optional http basic auth for restricting access to the website globally

# Todo

* cleanup code
  * XML comments
  * Having hardcoded russian strings in methods is pretty bad i guess
  * Divide up Program.cs its gotten pretty big
  * fix indent style in some older files
* UI adjustments
* Create a diary for the admin account
* Diaries
  * Diary entries
    * entry categories (tags)
  * diary styling
    * css
    * image upload
    * color changing
* BBCode
  * user tagging
  * community tagging
  * diary tagging
* Communities
  * Community diaries
* Topic new comments subscribe/unsubscribe ui
* public forums
* Multiple avatars
* Scale avatar down if its larger than allowed
* chats, private messages
* user image uploading (albums)
* quiz creation
* poll creation
* Optional invite-only registration
* Password resetting?
