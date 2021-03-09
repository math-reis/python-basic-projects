import copy
import random

class Hat:
    def __init__(self, **kwarg):
        contents = []
        for key in kwarg.keys():
            for n in range(kwarg[key]):
                contents.append(key)
        self.contents = contents

    def draw(self, number):
        contents = self.contents
        if number >= len(contents):
            return contents

        sample = []

        for n in range(number):
            len_contents = len(contents)
            index = random.randrange(len_contents)
            ball = contents[index]
            sample.append(ball)
            contents = contents[0:index] + contents[index + 1:]

        self.contents = contents
        return sample


def experiment(hat, expected_balls, num_balls_drawn, num_experiments):
    count = 0
    for n in range(num_experiments):
        example = copy.copy(hat)
        sample = example.draw(num_balls_drawn)
        success = True
        for key in expected_balls.keys():
            if sample.count(key) < expected_balls[key]:
                success = False
                break
        if success:
            count += 1

    return count / num_experiments
