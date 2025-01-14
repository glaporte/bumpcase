# bumpcase

basic calendar for appointments veterinarian system.

## Entities:
- Veterinarian (doctor)
- Customer (pet owner)
- Patient (pet)
- Slot (available appointments for customer)
- meeting (match with vetereniarion,customer,patient and slot)

## Implementation
  - Basic asp.net MVC app with swagger for test

## Logic
  - Some logic in `MeetingRepository` on `AddMeetingWithCustomDate/CancelMeeting/RescheduleMeeting` and SlotRepository in `SplitSlot/ResetAndMergeSlot` to allow a basic reservation system with split and merge based on date range.

