import 'package:flutter/material.dart';

class MatrixCell extends StatelessWidget {
  const MatrixCell({
    Key? key,
    required this.size,
    required this.value,
    required this.maxValue,
  }) : super(key: key);

  final Size size;
  final double value;
  final double maxValue;

  @override
  Widget build(BuildContext context) {
    const greenest = 255;
    final color = greenest * value / maxValue;
    final intColor = color.toInt();
    return SizedBox(
      width: size.width,
      height: size.height,
      child: Container(
        color: Color.fromRGBO(00, intColor, 00, 1.0),
      ),
    );
  }
}

class MatrixHeaderCell extends StatelessWidget {
  const MatrixHeaderCell({
    Key? key,
    required this.size,
    required this.text,
  }) : super(key: key);

  final Size size;
  final String text;

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: size.width,
      height: size.height,
      child: FittedBox(child: Text(text)),
    );
  }
}
