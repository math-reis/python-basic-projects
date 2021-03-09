class Rectangle:
    """Represents rectangular shapes.
    The Rectangle object is initialized with width and height of the rectangular
    shape. Area, perimeter, diagonal and the picture of the shape methods will
    be included.
    Attributes
    ----------
    width : int
        Width of the rectangle.
    height : int
        Height of the rectangle.
    """

    def __init__(self, width, height):
        """Initializes the Rectangle object.
        Parameters
        ----------
        width : int
            An integer that represents the width of the shape.
        height : int
            An integer that represents the height of the shape.
        """
        self.width = width
        self.height = height

    def set_width(self, width):
        """Sets width of the Rectangle object.
        Parameters
        ----------
        width : int
            Width of the Rectangle object.
        """
        self.width = width

    def set_height(self, height):
        """Sets height of the Rectangle object.
        Parameters
        ----------
        height : int
            Height of the Rectangle object
        """
        self.height = height

    def get_width(self):
        """Gets width of the Rectangle object.
        Returns
        -------
        int
            Width of the object.
        """
        return self.width

    def get_height(self):
        """Gets height of the Rectangle object.
        Returns
        -------
        int
            Height of the object.
        """
        return self.height

    def get_area(self):
        """Gets the area of the Rectangular object.
        Returns
        -------
        int
            Area given by width * height.
        """
        return self.width * self.height

    def get_perimeter(self):
        """Gets the perimeter of the Rectangular object.
        Returns
        -------
        int
            Perimeter given by (2 * width) + (2 * height).
        """
        return (2 * self.width) + (2 * self.height)

    def get_diagonal(self):
        """Gets the diagonal of the Rectangular object.
        Returns
        -------
        float
            Diagonal given by (width ** 2 + height ** 2) ** 0.5.
        """
        return (self.width ** 2 + self.height ** 2) ** 0.5

    def get_picture(self):
        """Draws the shape of the Rectangular object using '*' characters.
        The number of lines drawn is equal to the height of the object.
        The number of '*' in each line is equal to the width of the object.
        Returns
        -------
        str
            Shape (picture) of the object if height or width are less than
            or equal to 50
            Error  message otherwise.
        """
        if (self.width > 50):
            return "Too big for picture."

        if (self.height > 50):
            return "Too big for picture."
        
        line = '*' * self.width # Number of '*' in each line

        lines = [line for _ in range(self.height)] # Number of lines in shape

        picture = '\n'.join(lines)

        return picture + "\n"

    def get_amount_inside(self, shape):
        """Determines number of times a 'shape' fits into the Rectangle object.
        Parameters
        ----------
        shape : obj
            An instance of a Rectangular of Square class.
        Returns
        -------
        int
            Number of times the 'shape' fits inside this object.
        """
        w = self.width // shape.width
        h = self.height // shape.height

        return w * h

    def __str__(self):
        """String representation of the Rectangle object.
        
        Returns
        -------
        str
            A string giving the 'width' and 'height' of the object.
        """
        return "Rectangle(width=" + str(self.width) + ", height=" + str(self.height) + ")"

class Square(Rectangle):
    """Represents square shapes.
    The square object is initialized with a side length of the sqare shape.
    Attributes
    ----------
    side : int
        Side length of the square.
    """

    def __init__(self, side):
        """Initializes the Square object.
        Parameters
        ----------
        side : int
            An integer of side length of the square.
        
        """
        self.width = side
        self.height = side

    def set_side(self, side):
        """Sets the sides of the Square object.
        Parameter
        ---------
        side : int
            Side length of the square.
        """
        self.width = side
        self.height = side

    def __str__(self):
        """String representation of the Square object.
        Returns
        -------
        str
            A string giving the side of the Square object.
        """
        return "Square(side=" + str(self.width) + ")"
      
