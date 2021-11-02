import 'package:flutter/material.dart';
import 'package:freq_matrix/components/matrix_cell.dart';

import 'matrix_data_source.dart';

class Matrix extends StatefulWidget {
  const Matrix({
    Key? key,
    required this.size,
    required this.grams,
    required this.alphabet,
  }) : super(key: key);

  final String alphabet;
  final List<Gram> grams;
  final Size size;

  @override
  State<Matrix> createState() => _MatrixState();
}

class _MatrixState extends State<Matrix> {
  Size calculateCellSize(Size matrixSize, int alphabetLength) {
    return Size(
      matrixSize.width / (alphabetLength + 1),
      matrixSize.height / (alphabetLength + 1),
    );
  }

  List<Widget> _buildHorizontalHeaderCells(
    String alphabet,
    Size cellSize,
  ) {
    final headerChars = ['', ...alphabet.split('')];
    final cells = headerChars
        .map((ch) => MatrixHeaderCell(text: ch, size: cellSize))
        .toList();
    return cells;
  }

  @override
  Widget build(BuildContext context) {
    final grams = widget.grams;
    final alphabet = widget.alphabet;
    final cellSize = calculateCellSize(
      widget.size,
      alphabet.length,
    );
    final List<TableRow> rows = [
      TableRow(
        children: _buildHorizontalHeaderCells(
          alphabet,
          cellSize,
        ),
      ),
    ];
    final maxValue = grams.isEmpty ? 1.0 : grams.first.value;
    for (var a = 0; a < alphabet.length; a++) {
      final aSym = alphabet[a];
      final rowChildren = <Widget>[
        MatrixHeaderCell(text: aSym, size: cellSize),
      ];
      for (var b = 0; b < alphabet.length; b++) {
        final bSym = alphabet[b];
        final gramKey = aSym + bSym;
        final gramIndex = grams.indexWhere((gr) => gr.key == gramKey);
        final gramValue = gramIndex == -1 ? 0.0 : grams[gramIndex].value;
        rowChildren.add(
          MatrixCell(
            size: cellSize,
            value: gramValue,
            maxValue: maxValue,
          ),
        );
      }
      rows.add(TableRow(children: rowChildren));
    }
    return Table(
      children: rows,
    );
  }
}
