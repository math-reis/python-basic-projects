def add_time(start, duration, day=None):
    clock, period = start.split()
    initial_period = period

    hour, minute = clock.split(':')
    addHour, addMinute = duration.split(':')

    min_new = int(minute) + int(addMinute)
    hr_new = int(hour) + int(addHour)

    periods_later = 0
    days_later = 0

    if min_new > 59:
        min_new -= 60
        hr_new += 1

    hr_new_period = hr_new

    while hr_new > 12:
        hr_new -= 12

    while hr_new_period > 11:
        hr_new_period -= 12
        period = "PM" if period == "AM" else "AM"
        periods_later += 1

    if periods_later % 2 != 0:
        if initial_period == "PM":
            periods_later += 1
        else:
            periods_later -= 1

    days_later = periods_later / 2

    new_time = f"{hr_new}:{str(min_new).zfill(2)} {period}"

    DAYS_OF_WEEK = [
        "Saturday",
        "Sunday",
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday"
    ]

    if day:
        day_index = DAYS_OF_WEEK.index(day.title())
        new_day_index = int((day_index + days_later) % 7)
        new_time += f", {DAYS_OF_WEEK[new_day_index]}"

    if days_later == 1:
        new_time += " (next day)"

    if days_later > 1:
        new_time += f" ({int(days_later)} days later)"

    return new_time
    
