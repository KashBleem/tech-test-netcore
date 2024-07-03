# Recent Project Changes Summary
# Kash Bleem - July 2024 for StoryTeller
# Time taken - approx 4 hours 30 mins

## 2. Todo Item Ordering
- **File:** `Views/TodoList/Detail.cshtml`
- **Change:** Items are now listed by order of importance: High, Medium, Low.

## 3. Bug Fix: TodoItem to TodoItemEditFields Mapping
- **Issue:** The `Importance` field was not being copied correctly.
- **Action:** Fixed the mapping process and ensured all unit tests pass.

## 4. UI Improvement: Friendly Text for ResponsiblePartyId
- **Change:** Edit and create item pages now show user-friendly text (email) instead of "ResponsiblePartyId".

## 5. New Feature: Hide Completed Items
- **Location:** Details page
- **Description:** Added an option to hide items marked as done.

## 6. Enhanced TodoList Display
- **Change:** `/TodoList` now shows:
  - Todo-lists owned by the user
  - Todo-lists where the user is marked as the responsible party for at least one item

## 7. New Property: Rank for TodoItem
- **Changes:**
  - Added `Rank` property to `TodoItem` class
  - Created EntityFramework migration for the new property
  - Updated edit page to allow setting the rank
  - Added option on details page to order items by rank, importance and title

## 8. User Profile Enhancement
- **Feature:** Integration with Gravatar API
- **Changes:**
  - Download and display user's name from Gravatar profile
  - Show name alongside email address
- **Consideration:** Implemented handling for slow or non-responsive Gravatar service

## 9. Improved Item Addition Process
- **Location:** List detail page
- **Change:** Replaced "Add New Item" link with in-page UI for item creation
- **Implementation:** Used Javascript and a newly created API

## 10. Rank Adjustment API and UI
- **New API:** Allows setting of the `Rank` property
- **New UI:** Allows direct update of `Rank` property via API / AJAX; ordering order done on previous changeset

## Other items
- Updated README.md to include sqlite info to assist others
- Includes Easter egg :)

## Future - AKA if I had more time
- Thoroughly test all new features and changes, some bugs exist...
- Separate out API layer entirely
- Use named procedures/DBOs rather than EF entirely
- Add Logging
- UI Changes to jazz it up
- Update user documentation to reflect new functionalities
- Consider user feedback for further improvements